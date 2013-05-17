using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMatrix
{
    public static class Matrix
    {
        private const int NumberOfDirections = 8;
        private const int NumberOfLastDirection = 7;

        private static void ChangeDirection(ref int currentXDirection, ref int currentYDirection)
        {
            int[] xDirections = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] yDirections = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int currentDirection = 0;

            for (int i = 0; i < NumberOfDirections; i++)
            {
                if (xDirections[i] == currentXDirection &&
                    yDirections[i] == currentYDirection)
                {
                    currentDirection = i;
                    break;
                }
            }

            if (currentDirection == NumberOfLastDirection)
            {
                currentXDirection = xDirections[0];
                currentYDirection = yDirections[0];
                return;
            }

            currentXDirection = xDirections[currentDirection + 1];
            currentYDirection = yDirections[currentDirection + 1];
        }

        private static bool CheckForPossibleDirection(int[,] arr, int row, int col)
        {
            int[] xDirections = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] yDirections = { 1, 0, -1, -1, -1, 0, 1, 1 };

            for (int i = 0; i < NumberOfDirections; i++)
            {
                if (row + xDirections[i] >= arr.GetLength(0) || row + xDirections[i] < 0)
                {
                    xDirections[i] = 0;
                }

                if (col + yDirections[i] >= arr.GetLength(0) || col + yDirections[i] < 0)
                {
                    yDirections[i] = 0;
                }
            }

            for (int i = 0; i < NumberOfDirections; i++)
            {
                if (arr[row + xDirections[i], col + yDirections[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static void FindAvailableCell(int[,] matrix, out int currentRow, out int currentCol)
        {
            currentRow = 0;
            currentCol = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        currentRow = i;
                        currentCol = j;
                        return;
                    }
                }
            }
        }

        public static int[,] FillMatrix(int n)
        {
            int[,] matrix = new int[n, n];
            int currentNumber = 1, currentRow = 0, currentCol = 0;

            FillCellsUntilBlockage(matrix, n, ref currentNumber, ref currentRow, ref currentCol);

            FindAvailableCell(matrix, out currentRow, out currentCol);

            if (currentRow != 0 && currentCol != 0)
            {
                FillCellsUntilBlockage(matrix, n, ref currentNumber, ref currentRow, ref currentCol);
            }

            return matrix;
        }

        private static void FillCellsUntilBlockage(int[,] matrix, int n, ref int currentNumber, ref int currentRow, ref int currentCol)
        {
            matrix[currentRow, currentCol] = currentNumber;
            currentNumber++;
            int currentXDirection = 1, currentYDirection = 1;

            int nextXStep;
            int nextYStep;
            while (CheckForPossibleDirection(matrix, currentRow, currentCol))
            {
                nextXStep = currentRow + currentXDirection;
                nextYStep = currentCol + currentYDirection;
                if (!IsInRange(nextXStep, n) || !IsInRange(nextYStep, n) || matrix[nextXStep, nextYStep] != 0)
                {
                    while (!IsInRange(nextXStep, n) || !IsInRange(nextYStep, n) || matrix[nextXStep, nextYStep] != 0)
                    {
                        ChangeDirection(ref currentXDirection, ref currentYDirection);
                        nextXStep = currentRow + currentXDirection;
                        nextYStep = currentCol + currentYDirection;
                    }
                }

                currentRow += currentXDirection;
                currentCol += currentYDirection;
                matrix[currentRow, currentCol] = currentNumber;
                currentNumber++;

            }
        }

        private static bool IsInRange(int value, int n)
        {
            if (value >= n || value < 0)
            {
                return false;
            }
            return true;
        }
    }
}
