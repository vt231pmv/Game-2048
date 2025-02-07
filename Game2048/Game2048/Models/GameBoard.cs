using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Models
{
    public class GameBoard : ViewModel
    {
        public readonly int boardSize;
        public readonly int WinValue = 2048;
        private readonly BoardManipulator manipulator;

        public int[,] board;
        public int score;

        public int[,] Board { get => board; set => Set(ref board, value); }

        public int Score { get => score; set => Set(ref score, value); }

        public GameBoard(int size = 4)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            manipulator = new BoardManipulator(boardSize);
            ResetBoard();
        }

        public void ResetBoard()
        {
            board = new int[boardSize, boardSize];
            score = 0;
        }
        public bool Shift(Direction direction)
        {
            bool shifted = false;

            for (int i = 0; i < boardSize; i++)
            {
                int[] line = manipulator.GetLine(board, i, direction);
                int[] mergedLine = manipulator.MergeLine(line, ref score);
                if (!shifted) shifted = !manipulator.AreLinesEqual(line, mergedLine);
                manipulator.SetLine(board, i, mergedLine, direction);
            }

            return shifted;
        }
        public void AddRandomTile(Random random)
        {
            List<(int, int)> emptyCells = new();
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                    if (board[i, j] == 0) emptyCells.Add((i, j));

            if (emptyCells.Count > 0)
            {
                var (x, y) = emptyCells[random.Next(emptyCells.Count)];
                board[x, y] = random.NextDouble() < 0.9 ? 2 : 4;
            }
        }
    }
}
