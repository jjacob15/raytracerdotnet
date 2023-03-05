using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix
    {
        public int Size;
        private double[,] _m;
        public static Matrix Identity = new Matrix(1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);


        public Matrix(double m00, double m01,
            double m10, double m11)
        {
            Size = 2;
            _m = new double[2, 2];

            _m[0, 0] = m00;
            _m[0, 1] = m01;

            _m[1, 0] = m10;
            _m[1, 1] = m11;
        }

        public Matrix(double m00, double m01, double m02,
            double m10, double m11, double m12,
            double m20, double m21, double m22)
        {
            Size = 3;
            _m = new double[3, 3];

            _m[0, 0] = m00;
            _m[0, 1] = m01;
            _m[0, 2] = m02;

            _m[1, 0] = m10;
            _m[1, 1] = m11;
            _m[1, 2] = m12;

            _m[2, 0] = m20;
            _m[2, 1] = m21;
            _m[2, 2] = m22;

        }

        public Matrix(double m00, double m01, double m02, double m03,
            double m10, double m11, double m12, double m13,
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33)
        {
            Size = 4;
            _m = new double[4, 4];

            _m[0, 0] = m00;
            _m[0, 1] = m01;
            _m[0, 2] = m02;
            _m[0, 3] = m03;

            _m[1, 0] = m10;
            _m[1, 1] = m11;
            _m[1, 2] = m12;
            _m[1, 3] = m13;

            _m[2, 0] = m20;
            _m[2, 1] = m21;
            _m[2, 2] = m22;
            _m[2, 3] = m23;

            _m[3, 0] = m30;
            _m[3, 1] = m31;
            _m[3, 2] = m32;
            _m[3, 3] = m33;
        }
        public Matrix(int size)
        {
            Size = size;
            _m = new double[size, size];
        }
        public Matrix(int size, double intializeWith)
        {
            Size = size;
            _m = new double[size, size];

            Initialize(intializeWith);
        }

        private void Initialize(double val)
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    _m[r, c] = val;
        }
        public double this[int row, int column]
        {
            get => _m[row, column];
            set => _m[row, column] = value;
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
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    m[r, c] = _m[c, r];
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

        public double Determinant()
        {
            if (Size == 2)
                return _m[0, 0] * _m[1, 1] - _m[0, 1] * _m[1, 0];
            else
            {
                double det = 0;
                for (int c = 0; c < Size; c++)
                {
                    det += _m[0, c] * Cofactor(0, c);
                }
                return det;
            }
        }

        public Matrix Submatrix(int removeRow, int removeCol)
        {
            if (Size == 2) throw new Exception("Cannot reduce this matrix");
            Matrix submatrix = new Matrix(Size - 1);
            int newRow = 0, newCol = 0;
            for (int r = 0; r < Size; r++)
            {
                if (r == removeRow) continue;
                newCol = 0;
                for (int c = 0; c < Size; c++)
                {
                    if (c == removeCol) continue;
                    submatrix[newRow, newCol++] = _m[r, c];
                }
                newRow++;
            }
            return submatrix;
        }

        public double Minor(int row, int col)
        {
            return Submatrix(row, col).Determinant();
        }

        public double Cofactor(int row, int col)
        {
            return Minor(row, col) * ((row + col) % 2 != 0 ? -1 : 1);
        }
    }
}
