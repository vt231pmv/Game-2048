using Game2048.Commands;
using Game2048.Data;
using Game2048.Models;
using Game2048.ViewModels.Base;
using Game2048.Views.Window;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Game2048.ViewModels
{
    class GameViewModel : ViewModel
    {

        private GameBoard gameBoard;
        private Random random;

        public int[,] Board { get => gameBoard.board; private set => Set(ref gameBoard.board, value); }
        public int Score { get => gameBoard.score; private set => Set(ref gameBoard.score, value); }

        public GameViewModel()
        {
            gameBoard = new GameBoard();
            random = new Random();

            Reset();
        }

        #region Commands
        public NavigationCommand NavigateToMenuPage { get => new(NavigateToPage, new Uri("Views/Pages/MenuPage.xaml", UriKind.RelativeOrAbsolute)); }

        public RelayCommand ShiftLeftCommand => new(() => ShiftBoard(Direction.Left));
        public RelayCommand ShiftRightCommand => new(() => ShiftBoard(Direction.Right));
        public RelayCommand ShiftDownCommand => new(() => ShiftBoard(Direction.Down));
        public RelayCommand ShiftUpCommand => new(() => ShiftBoard(Direction.Up));
        public RelayCommand ResetCommand { get => new(Reset); }
        #endregion

        #region Operations
        private void Reset()
        {
            Board = new int[gameBoard.boardSize, gameBoard.boardSize];
            Score = 0;
            GenerateRandomNumber();
            GenerateRandomNumber();
            Update();
        }
        private void GenerateRandomNumber()
        {
            int row, col;
            do
            {
                row = random.Next(gameBoard.boardSize);
                col = random.Next(gameBoard.boardSize);
            } while (gameBoard.board[row, col] != 0);

            gameBoard.board[row, col] = random.Next(100) < 90 ? 2 : 4;
        }

        private void Update()
        {
            Board = gameBoard.Board;
            Score = gameBoard.Score;
        }
        #endregion

        #region GameState
        private void CheckGameState()
        {
            Update();
            if (IsGameOver())
            {
                MessageBoxResult result = MessageBox.Show("Ви програли! Бажаєте занести себе до списку?", "Кінець", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    AddToStatistics();
                }
                Reset();
            }
            else if (IsGameWin())
            {
                MessageBoxResult result = MessageBox.Show("Ви виграли! Бажаєте занести себе до списку?", "Кінець", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    AddToStatistics();
                }
                Reset();
            }
        }

        public bool IsGameWin()
        {
            for (int row = 0; row < gameBoard.boardSize; row++)
            {
                for (int column = 0; column < gameBoard.boardSize; column++)
                {
                    if (gameBoard.board[row, column] == gameBoard.WinValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsGameOver()
        {
            for (int row = 0; row < gameBoard.boardSize; row++)
            {
                for (int column = 0; column < gameBoard.boardSize; column++)
                {
                    if (gameBoard.board[row, column] == 0)
                    {
                        return false;
                    }
                }
            }

            for (int row = 0; row < gameBoard.boardSize; row++)
            {
                for (int column = 0; column < gameBoard.boardSize; column++)
                {

                    int value = gameBoard.board[row, column];

                    if (row > 0 && gameBoard.board[row - 1, column] == value)
                    {
                        return false;
                    }

                    if (row < gameBoard.boardSize - 1 && gameBoard.board[row + 1, column] == value)
                    {
                        return false;
                    }

                    if (column > 0 && gameBoard.board[row, column - 1] == value)
                    {
                        return false;
                    }

                    if (column < gameBoard.boardSize - 1 && gameBoard.board[row, column + 1] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Statistics
        private void AddToStatistics()
        {
            string name;

            do
            {
                name = Microsoft.VisualBasic.Interaction.InputBox("Введіть ваше ім'я: ", "Введення імені", "");
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Ім'я не може бути порожнім. Будь ласка, введіть ваше ім'я!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } while (string.IsNullOrEmpty(name));

            Statistics.Add(name, Score.ToString());
        }
        #endregion

        #region Shifts
        private void ShiftBoard(Direction direction)
        { 
            if (gameBoard.Shift(direction))
            {
                GenerateRandomNumber();
                CheckGameState();
            }
        }
        #endregion
    }
}
