using System;

namespace MatrixTasks
{
    class Program
    {
        // Function that shows an Array.
        public static void ShowArray(double[] array)
        {
            foreach (double item in array)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Constructing First Matrix.
            Console.WriteLine("Enter the Number of Rows: ");
            int rows = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Number of Columns: ");
            int columns = int.Parse(Console.ReadLine());
            Matrix matrix1 = new Matrix(rows, columns);
            Console.WriteLine(matrix1);

            // Constructing Second Matrix.
            Matrix matrix2 = new Matrix();
            Console.WriteLine(matrix2);

            Console.WriteLine("Addition:\n" + (matrix1 + matrix2));
            Console.WriteLine("Multiplication:\n" + matrix1 * matrix2);
            Console.WriteLine("Scalar Multiplication:\n" + 5 * matrix1);

            Console.WriteLine(matrix1.Transpose());
            Console.WriteLine(matrix2.Invserse());

            // Constructing an array for transformations.
            double[] array = new double[4] { 4, 5, 6, 1 };
            // Scaling.
            Console.WriteLine("Scaled Array:\n");
            ShowArray(Matrix.Scale(1, 2, 3, array));
            // Translating.
            Console.WriteLine("Translated Array:\n");
            ShowArray(Matrix.Translate(1, 2, 3, array));
            // Rotating.
            Console.WriteLine("Rotated Array:\n");
            // Create a Rotation Matrix Around the x axis.
            ShowArray(Matrix.RotateX(90, array));
            // Create a Rotation Matrix Around the y axis.
            ShowArray(Matrix.RotateY(90, array));
            // Create a Rotation Matrix Around the z axis.
            ShowArray(Matrix.RotateZ(90, array));
        }
    }
}
