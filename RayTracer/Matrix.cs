using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Matrix
    {
        public int Size;
        private double[,] _m;

        public static Matrix Identity() => new Matrix(1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public static Matrix ViewTransform(Tuple from, Tuple to, Tuple up)
        {
            var forward = (to - from).Normalize();
            var upNorm = up.Normalize();
            var left = forward.Cross(upNorm);
            var trueUp = left.Cross(forward);

            Matrix orientation = new Matrix(left.X, left.Y, left.Z, 0,
                trueUp.X, trueUp.Y, trueUp.Z, 0,
                -forward.X, -forward.Y, -forward.Z, 0,
                0, 0, 0, 1);

            return orientation * Identity().Translation(-from.X, -from.Y, -from.Z).Apply();
        }

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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Matrix m = (Matrix)obj;
                return m == this;
            }
        }
        public override int GetHashCode()
        {
            double hashcode = 23;
            for (var r = 0; r < Size; r++)
                for (var c = 0; c < Size; c++)
                    hashcode = (hashcode * 37) + _m[r, c];

            return (int)Math.Round(hashcode);
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

        public static Tuple operator *(Matrix a, Tuple b) => new Tuple(ColProduct(0, a, b), ColProduct(1, a, b), ColProduct(2, a, b), ColProduct(3, a, b));

        public static Tuple operator *(Tuple b, Matrix a) => new Tuple(ColProduct(0, a, b), ColProduct(1, a, b), ColProduct(2, a, b), ColProduct(3, a, b));

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
            int newRow = 0;
            for (int r = 0; r < Size; r++)
            {
                if (r == removeRow) continue;
                int newCol = 0;
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

        public bool IsInvertable()
        {
            return Determinant() != 0;
        }


        public Matrix Inverse()
        {
            if (!IsInvertable()) throw new NotSupportedException("This matrix is not invertable");

            Matrix result = new Matrix(Size);
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    var c = Cofactor(row, col);
                    result[col, row] = c / Determinant();
                }
            }
            return result;
        }

        private Stack<Matrix> _transformationChain = new Stack<Matrix>();

        public Matrix Apply()
        {
            Matrix result = this;
            while (_transformationChain.Count > 0)
            {
                var transform = _transformationChain.Pop();
                result = result * transform;
            }
            return result;
        }

        public Matrix Scaling(double x, double y, double z)
        {
            var transform = Identity();
            transform[0, 0] = x;
            transform[1, 1] = y;
            transform[2, 2] = z;

            _transformationChain.Push(transform);
            return this;
        }

        public Matrix Translation(double x, double y, double z)
        {
            var transform = Identity();

            transform[0, 3] = x;
            transform[1, 3] = y;
            transform[2, 3] = z;

            _transformationChain.Push(transform);
            return this;
        }

        public Matrix RotateX(double r)
        {
            var transform = Identity();
            transform[1, 1] = Math.Cos(r);
            transform[1, 2] = -Math.Sin(r);
            transform[2, 1] = Math.Sin(r);
            transform[2, 2] = Math.Cos(r);

            _transformationChain.Push(transform);
            return this;
        }

        public Matrix RotateY(double r)
        {
            var transform = Identity();
            transform[0, 0] = Math.Cos(r);
            transform[0, 2] = Math.Sin(r);
            transform[2, 0] = -Math.Sin(r);
            transform[2, 2] = Math.Cos(r);

            _transformationChain.Push(transform);
            return this;
        }

        public Matrix RotateZ(double r)
        {
            var transform = Identity();
            transform[0, 0] = Math.Cos(r);
            transform[0, 1] = -Math.Sin(r);
            transform[1, 0] = Math.Sin(r);
            transform[1, 1] = Math.Cos(r);

            _transformationChain.Push(transform);
            return this;
        }

        public Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            var transform = Identity();
            transform[0, 1] = xy;
            transform[0, 2] = xz;
            transform[1, 0] = yx;
            transform[1, 2] = yz;
            transform[2, 0] = zx;
            transform[2, 1] = zy;

            _transformationChain.Push(transform);
            return this;
        }
    }
}
