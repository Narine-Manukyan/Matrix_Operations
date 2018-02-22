using System;

namespace MatrixTasks
{
    /// <summary>
    /// This Class Implements a Matrix Construction.
    /// </summary>
    public class Matrix
    {
        private readonly int Rows;
        private readonly int Columns;
        double[,] matrix;

        // Parameterless Constructor, which constructs Matrix with user's input.
        public Matrix()
        {
            Console.WriteLine("Enter the Number of Rows: ");
            this.Rows = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Number of Columns: ");
            this.Columns = int.Parse(Console.ReadLine());

            // Initialise matrix.
            this.matrix= new double[this.Rows,this.Columns];
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    this[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
        }

        // Constructor with 1 parameter, which constructs Matrix with passed int[,] matrix.
        public Matrix(double[,] matrix)
        {
            this.Rows = matrix.GetLength(0);
            this.Columns = matrix.GetLength(1);
            this.matrix = matrix;
        }

        // Constructor with 2 parameters, this Constructor generates Matrix elements Randomly.
        public Matrix(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.matrix = new double[Rows, Columns];

            // Random number generator.
            Random random = new Random();
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    this[i, j] = random.Next(-99, 100);
                }
            }
        }

        // Indexator for Matrix Elements.
        public double this[int rows, int columns]
        {
            get
            {
                if (rows < 0 || rows > this.Rows)
                {
                    throw new Exception("Row is out of range!");
                }
                if (columns < 0 || columns > this.Columns)
                {
                    throw new Exception("Columns is out of range!");
                }
                return this.matrix[rows, columns];
            }
            set
            {
                this.matrix[rows, columns] = value;
            }
        }

        // This Function copies Matrix.
        public Matrix Copy()
        {
            Matrix copyMatrix = new Matrix(this.Rows, this.Columns);        
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    //Creating the copy of Matrix.
                    copyMatrix[i, j] = this[i, j];
                }
            }
            return copyMatrix;
        }

        // Define a Identity Matrix.
        public Matrix Identity()
        {
            // Creating the Identity Matrix.
            Matrix identityMatrix = new Matrix(this.Rows, this.Columns);
     
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    if (i == j)
                        identityMatrix[i, j] = 1;
                    else
                        identityMatrix[i, j] = 0;
                }
            }
            return identityMatrix;
        }

        // Define a Identity Matrix By size.
        public static Matrix Identity(int size)
        {
            // Creating the Identity Matrix.
            Matrix identityMatrix = new Matrix(size, size);

            for (int i = 0; i < identityMatrix.Rows; i++)
            {
                for (int j = 0; j < identityMatrix.Columns; j++)
                {
                    if (i == j)
                        identityMatrix[i, j] = 1;
                    else
                        identityMatrix[i, j] = 0;
                }
            }
            return identityMatrix;
        }

        // Checking if Matrix is Squared.
        public bool IsSquared()
        {
            if (this.Rows == this.Columns)
                return true;
            else
                return false;
        }

        // Declaring operator+.
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            // Checking dimensions of Matrixes.
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
            {
                throw new Exception("Matrixes must have the same dimensions!");
            }
            // Matrix which describes Addition of two Matrixes.
            Matrix sumMatrix = new Matrix(matrix1.Rows, matrix1.Columns);
            for (int i = 0; i < sumMatrix.Rows; i++)
            {
                for (int j = 0; j < sumMatrix.Columns; j++)
                {
                    sumMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return sumMatrix;
        }

        // Declaring operator*.
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            // Checking dimensions of Matrixes.
            if (matrix1.Columns != matrix2.Rows)
            {
                throw new Exception("Wrong dimensions of Matrix!");
            }
            // Matrix which describes a Multiplication of two Matrixes.
            Matrix multMatrix = new Matrix(matrix1.Rows, matrix2.Columns);
            for (int i = 0; i < multMatrix.Rows; i++)
                for (int j = 0; j < multMatrix.Columns; j++)
                    for (int k = 0; k < matrix1.Columns; k++)
                        multMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
            return multMatrix;
        }

        // Declaring operator*(Scalar Multiplication).
        public static Matrix operator *(double scalarValue, Matrix matrix)
        {
            // Matrix which describes Scalar Multiplication of two Matrixes.
            Matrix multMatrix = new Matrix(matrix.Rows, matrix.Columns);
            for (int i = 0; i < multMatrix.Rows; i++)
                for (int j = 0; j < multMatrix.Columns; j++)
                    multMatrix[i, j] = scalarValue * matrix[i, j];
            return multMatrix;
        }

        // Declaring operator*(Vector Multiply).
        public static double[] operator *(double[] array, Matrix matrix)
        {
            // Vector Multiply.
            double[] returnVector = new double[array.Length];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    returnVector[i] += array[j] * matrix[i, j]; 
                }
            }
            return returnVector;
        }

        // Constructing Transpose Matrix For a Given Matrix .
        public Matrix Transpose()
        {
            // Matrix which describes Transpose of Matrix.
            Matrix transposeMatrix = new Matrix(this.Columns, this.Rows);
            for (int i = 0; i < transposeMatrix.Rows; i++)
            {
                for (int j = 0; j < transposeMatrix.Columns; j++)
                {
                    transposeMatrix[i, j] = this[j, i];
                }
            }
            return transposeMatrix;
        }

        // Inverse matrix, using Gauss–Jordan elimination.
        public Matrix Invserse()
        {
            if (this.IsSquared())
            {
                // Size of Squared matrix.
                int size = this.Rows;

                // Creating Extended Matrix with Left and Right sides.
                Matrix extendedMatrix = new Matrix(2 * size, 2 * size);

                // Initializing Right side to identity matrix
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size * 2; j++)
                    {
                        if (j == (i + size))
                            extendedMatrix[i, j] = 1;
                        else
                            extendedMatrix[i, j] = 0;
                    }
                }

                // Initializing Left side to Matrix.
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        extendedMatrix[i, j] = this[i, j];
                    }
                }

                Console.WriteLine("Extended Matrix:\n" + extendedMatrix);

                // Pivoting.
                double temp;
                for (int i = size; i > 1; i--)
                {
                    if (extendedMatrix[i - 1,1] < extendedMatrix[i,1])
                    {
                        for (int j = 0; j < 2 * size; j++)
                        {
                            temp = extendedMatrix[i,j];
                            extendedMatrix[i,j] = extendedMatrix[i - 1,j];
                            extendedMatrix[i - 1, j] = temp;
                        }
                    }
                }
                Console.WriteLine("Pivoting:\n" + extendedMatrix);

                // Reducing To Diagonal Matrix.
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < 2 * size; j++)
                    {
                        if (j != i)
                        {
                            if (extendedMatrix[i, i] != 0)
                            {
                                temp = extendedMatrix[j, i] / extendedMatrix[i, i];
                                for (int k = 0; k < size * 2; k++)
                                {
                                    extendedMatrix[j, k] -= extendedMatrix[i, k] * temp;
                                }
                            }
                            else
                                throw new Exception("Devide by 0 Exception...");
                        }
                    }
                }

                // Reducing To Unit Matrix.
                for (int i = 0; i < size; i++)
                {
                    temp = extendedMatrix[i, i];
                    if(temp == 0)
                        throw new Exception("Devide by 0 Exception...");
                    for (int j = 0; j < 2 * size; j++)
                    {
                        extendedMatrix[i,j] = extendedMatrix[i, j] / temp;
                    }
                }

                // Constructing Inverse Matrix.
                Matrix returnMatrix = new Matrix(size, size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = size; j < 2 * size; j++)
                    {
                        returnMatrix[i, j - size] = extendedMatrix[i, j];
                    }
                }
                return returnMatrix;
            }
            else
                throw new Exception("The Matrix must be squared...");
        }

        // Determining whether a Matrix is Orthogonal or Not.
        // A Matrix is orthogonal if its Transpose is equal to its Inverse.
        public bool IsOrthogonal()
        {
            if (this.Invserse().Rows != this.Transpose().Rows 
                || this.Invserse().Columns != this.Transpose().Columns)
                    return false;
            for (int i = 0; i < this.Invserse().Rows; i++)
            {
                for (int j = 0; j < this.Invserse().Columns; j++)
                {
                    if (this.Invserse()[i, j] != this.Transpose()[i, j])
                        return false;
                }
            }
            return true;
        }

        // Create a Scaling Vector.
        public static double[] Scale(double sx, double sy, double sz, double[] array)
        {
            // Creating the Identity Matrix.
            Matrix identity = Identity(array.Length);
            // Set an Appropriate values.
            identity[0, 0] = sx;
            identity[1, 1] = sy;
            identity[2, 2] = sz;
            // Vector Multiply.
            return array * identity;
        }

        // Create a Translating Vector.
        public static double[] Translate(double tx, double ty, double tz, double[] array)
        {
            // Creating the Identity Matrix.
            Matrix identity = Identity(array.Length);
            // Set an Appropriate values.
            identity[0, identity.Columns - 1] = tx;
            identity[1, identity.Columns - 1] = ty;
            identity[2, identity.Columns - 1] = tz;
            // Vector Multiply.
            return array * identity;
        }

        // Create a Rotation Matrix Around the x axis.
        public static double[] RotateX(double theta, double[] array)
        {
            // Defining sin and cos.
            theta *= Math.PI / 180;
            double sin = Math.Sin(theta);
            double cos = Math.Cos(theta);
            // Creating the Identity Matrix.
            Matrix identity = Identity(array.Length);
            // Set an Appropriate values.
            identity[1, 1] = cos;
            identity[1, 2] = -sin;
            identity[2, 1] = sin;
            identity[2, 2] = cos;
            // Vector Multiply.
            return array * identity;
        }

        // Create a Rotation Matrix Around the y axis.
        public static double[] RotateY(double theta, double[] array)
        {
            // Defining sin and cos.
            theta *= Math.PI / 180;
            double sin = Math.Sin(theta);
            double cos = Math.Cos(theta);
            // Creating the Identity Matrix.
            Matrix identity = Identity(array.Length);
            // Set an Appropriate values.
            identity[0, 0] = cos;
            identity[0, 2] = sin;
            identity[2, 0] = -sin;
            identity[2, 2] = cos;
            // Vector Multiply.
            return array * identity;
        }

        // Create a Rotation Matrix Around the z axis.
        public static double[] RotateZ(double theta, double[] array)
        {
            // Defining sin and cos.
            theta *= Math.PI / 180;
            double sin = Math.Sin(theta);
            double cos = Math.Cos(theta);
            // Creating the Identity Matrix.
            Matrix identity = Identity(array.Length);
            // Set an Appropriate values.
            identity[0, 0] = cos;
            identity[0, 1] = -sin;
            identity[1, 0] = sin;
            identity[1, 1] = cos;
            // Vector Multiply.
            return array * identity;
        }

        // Overriding ToString() Method for this Class.
        public override string ToString()
        {
            string stringOfMatrix = "\nMatrix:\n";
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    stringOfMatrix += this.matrix[i, j] + "\t";
                }
                stringOfMatrix += "\n";
            }
            return stringOfMatrix;
        }
    }
}
