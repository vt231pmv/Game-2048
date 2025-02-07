using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Models
{
    public class BoardManipulator
    {
        private readonly int boardSize;

        public BoardManipulator(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public int[] GetLine(int[,] board, int index, Direction direction)
        {
            int[] line = new int[boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                switch (direction)
                {
                    case Direction.Left:
                        line[i] = board[index, i];
                        break;
                    case Direction.Right:
                        line[i] = board[index, boardSize - 1 - i];
                        break;
                    case Direction.Up:
                        line[i] = board[i, index];
                        break;
                    case Direction.Down:
                        line[i] = board[boardSize - 1 - i, index];
                        break;
                }
            }
            return line;
        }

        public void SetLine(int[,] board, int index, int[] line, Direction direction)
        {
            for (int i = 0; i < boardSize; i++)
            {
                switch (direction)
                {
                    case Direction.Left:
                        board[index, i] = line[i];
                        break;
                    case Direction.Right:
                        board[index, boardSize - 1 - i] = line[i];
                        break;
                    case Direction.Up:
                        board[i, index] = line[i];
                        break;
                    case Direction.Down:
                        board[boardSize - 1 - i, index] = line[i];
                        break;
                }
            }
        }

        public int[] MergeLine(int[] line, ref int score)
        {
            List<int> merged = new();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 0) continue;

                if (merged.Count > 0 && merged.Last() == line[i])
                {
                    merged[merged.Count - 1] *= 2;
                    score += merged.Last();
                }
                else
                {
                    merged.Add(line[i]);
                }
            }

            while (merged.Count < boardSize)
            {
                merged.Add(0);
            }

            return merged.ToArray();
        }

        public bool AreLinesEqual(int[] line1, int[] line2)
        {
            return line1.SequenceEqual(line2);
        }
    }
}
