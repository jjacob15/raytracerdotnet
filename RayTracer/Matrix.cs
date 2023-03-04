using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix
    {
        public int Size;
        private double[,] _matrix = new double[4, 4];
        public static Matrix Identity = new Matrix(1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public Matrix(double m00, double m01, double m02, double m03,
            double m10, double m11, double m12, double m13,
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33)
        {
            Size = 4;
            _matrix = new double[4, 4];

            _matrix[0, 0] = m00;
            _matrix[0, 1] = m01;
            _matrix[0, 2] = m02;
            _matrix[0, 3] = m03;

            _matrix[1, 0] = m10;
            _matrix[1, 1] = m11;
            _matrix[1, 2] = m12;
            _matrix[1, 3] = m13;

            _matrix[2, 0] = m20;
            _matrix[2, 1] = m21;
            _matrix[2, 2] = m22;
            _matrix[2, 3] = m23;

            _matrix[3, 0] = m30;
            _matrix[3, 1] = m31;
            _matrix[3, 2] = m32;
            _matrix[3, 3] = m33;
        }
        public Matrix(int size)
        {
            Size = size;
        }
        public Matrix(int size, double intializeWith)
        {
            Size = size;
            _matrix = new double[size, size];

            Initialize(intializeWith);
        }

        private void Initialize(double val)
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    _matrix[r, c] = val;
        }
        public double this[int row, int column]
        {
            get => _matrix[row, column];
            set => _matrix[row, column] = value;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a.Size != b.Size) return false;

            for (int r = 0; r < a.Size; r++)
                for (int c = 0; c < a.Size; c++)
                    if (!a[r, c].DoubleEqual(b[r, c]))
                        return false;

            return true;
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.Size);
            for (int r = 0; r < a.Size; r++)
            {
                for (int c = 0; c < a.Size; c++)
                {
                    result[r, c] = ColProduct(r, c, a, b);
                }
            }
            return result;
        }

        public Matrix Transpose()
        {
            Matrix m = new Matrix(Size);
            for(int r = 0; r < Size; r++)
            {
                for(int c = 0; c < Size; c++)
                {
                    m[r, c] = _matrix[c, r];
                }
            }
            return m;
        }

        public static Tuple operator *(Matrix a, Tuple b)
        {
            return new Tuple(ColProduct(0, a, b), ColProduct(1, a, b), ColProduct(2, a, b), ColProduct(3, a, b));
        }

        private static double ColProduct(int r, Matrix a, Tuple b)
        {
            double result = 0;
            result += a[r, 0] * b.X;
            result += a[r, 1] * b.Y;
            result += a[r, 2] * b.Z;
            result += a[r, 3] * b.W;
            return result;
        }

        private static double ColProduct(int r, int c, Matrix a, Matrix b)
        {
            double result = 0;
            for (int i = 0; i < a.Size; i++)
            {
                result += a[r, i] * b[i, c];
            }
            return result;
        }
    }
}
