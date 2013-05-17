namespace JustMatrix
{
    using System;

    public class WalkInMatrix
    {
        public static void Main(string[] args)
        {
            int n = ReadInput();
            int[,] matrix = Matrix.FillMatrix(n);
            PrintMatrix(matrix);
        }

        private static int ReadInput()
        {
            Console.WriteLine("Enter a positive number ");
            string input = Console.ReadLine();
            int n = 0;
            while (!int.TryParse(input, out n) || n <= 0 || n > 100)
            {
                Console.WriteLine("You haven't entered a correct positive number");
                input = Console.ReadLine();
            }
            return n;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0, 3}", matrix[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
