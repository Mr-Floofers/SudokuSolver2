using System;
using System.IO;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] board = GetBoardFromFile("input.txt");

            bool result = Solve(board);

            if (result)
            {
                PrintBoard(board);
            }
            else
            {
                Console.WriteLine("Couldn't solve it.");
            }
        }

        private static bool Solve(int[][] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row][col] == 0)   // empty
                    {
                        for (int guess = 1; guess <= 9; guess++)
                        {
                            if (IsValid(board, row, col, guess))
                            {
                                // insert it into the board
                                board[row][col] = guess;

                                if (Solve(board))
                                {
                                    return true;
                                }

                                board[row][col] = 0;
                            }

                        }

                        return false;
                        // try guesses
                    }

                }
            }

            return true;
        }

        private static bool IsValid(int[][] board, int row, int col, int guess)
        {
            return IsRowValid(board, row, guess) && IsColValid(board, col, guess) && IsSquareValid(board, row, col, guess);
        }

        private static bool IsSquareValid(int[][] board, int row, int col, int guess)
        {
            int currentSquareRow = row - row % 3;//(Math.Abs(row - 1) / 3) % 3;
            int currentSquareCol = col - col % 3;//(Math.Abs(col - 1) / 3) % 3;

            for (int currRow = currentSquareRow; currRow < currentSquareRow + 3; currRow++)
            {
                for (int currCol = currentSquareCol; currCol < currentSquareCol + 3; currCol++)
                {
                    if (board[currRow][currCol] == guess)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsColValid(int[][] board, int col, int guess)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row][col] == guess)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsRowValid(int[][] board, int row, int guess)
        {
            return !board[row].Contains(guess);
        }

        private static void PrintBoard(int[][] board)
        {
            for (int x = 0; x < board.Length; x++)
            {
                for (int y = 0; y < board[x].Length; y++)
                {
                    Console.Write($"{board[x][y]} ");
                }
                Console.WriteLine();
            }
        }

        private static int[][] GetBoardFromFile(string filename)
        {
            int[][] board = new int[9][];

            string[] lines = File.ReadAllLines(filename);

            int currentRow = 0;
            foreach (var row in lines)
            {
                board[currentRow] = row.Select(c => c - '0').ToArray();
                currentRow++;

            }

            return board;
        }



    }
}
