using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class MatrixTests
    {
        [Fact]
        public void Initialization()
        {
            Matrix m = new Matrix(4);
            m[0, 0] = 1;
            m[0, 3] = 4;
            m[1, 0] = 5.5;
            m[1, 2] = 7.5;
            m[2, 2] = 11;
            m[3, 0] = 13.5;
            m[3, 2] = 15.5;

            Assert.True(m[3, 0].DoubleEqual(13.5));
        }

        [Fact]
        public void Initialization2x2()
        {
            Matrix m = new Matrix(2);
            m[1, 1] = 11;

            Assert.True(m[1, 1].DoubleEqual(11));
        }

        [Fact]
        public void Equality()
        {
            Matrix a = new Matrix(2, 5);
            Matrix b = new Matrix(2, 5);

            Assert.True(a == b);
        }

        [Fact]
        public void NotEquality()
        {
            Matrix a = new Matrix(2, 5);
            Matrix b = new Matrix(2, 6);

            Assert.True(a != b);
        }

        [Fact]
        public void Multiplication()
        {
            Matrix a = new Matrix(1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);
            Matrix b = new Matrix(-2, 1, 2, 3,
                3, 2, 1, -1,
                4, 3, 6, 5,
                1, 2, 7, 8);

            Matrix c = new Matrix(20, 22, 50, 48,
                44, 54, 114, 108,
                40, 58, 110, 102,
                16, 26, 46, 42);

            var result = a * b;

            Assert.True(result == c);
        }

        [Fact]
        public void MultiplyTuples()
        {
            Matrix a = new Matrix(1, 2, 3, 4,
               2, 4, 4, 2,
                8, 6, 4, 1,
                0, 0, 0, 1);
            Tuple b = new Tuple(1, 2, 3, 1);

            Tuple r = new Tuple(18, 24, 33, 1);

            var result = a * b;

            Assert.True(result.Equals(r));
        }

        [Fact]
        public void Transpose()
        {
            Matrix a = new Matrix(0, 9, 3, 0,
               9, 8, 0, 8,
                1, 8, 5, 3,
                0, 0, 5, 8);
            Matrix t = new Matrix(0, 9, 1, 0,
               9, 8, 8, 0,
                3, 0, 5, 5,
                0, 8, 3, 8);

            Assert.True(a.Transpose() == t);
        }

        [Fact]
        public void TransposeIdentity()
        {
            Matrix i1 = Matrix.Identity();
            Matrix i2 = Matrix.Identity();

            Assert.True(i1.Transpose() == i2);
        }

        [Fact]
        public void DeterminantTest2x2()
        {
            Matrix m = new Matrix(1, 5,
                -3, 2);

            Assert.True(m.Determinant().DoubleEqual(17));
        }

        [Fact]
        public void SubmatrixTest1()
        {
            Matrix m = new Matrix(1, 5, 0,
                -3, 2, 7,
                0, 6, -3);
            Matrix result = new Matrix(-3, 2,
                0, 6);
            Assert.True(result == m.Submatrix(0, 2));
        }

        [Fact]
        public void SubmatrixTest2()
        {
            Matrix m = new Matrix(-6, 1, 1, 6,
                -8, 5, 8, 6,
                -1, 0, 8, 2,
                -7, 1, -1, 1);

            Matrix result = new Matrix(-6, 1, 6,
                -8, 8, 6,
                -7, -1, 1);
            Assert.True(result == m.Submatrix(2, 1));
        }

        [Fact]
        public void MinorTest()
        {
            Matrix m = new Matrix(3, 5, 0,
                2, -1, -7,
                6, -1, 5);

            Assert.True(m.Minor(1, 0).DoubleEqual(25));
        }

        [Fact]
        public void CofactTest1()
        {
            Matrix m = new Matrix(3, 5, 0,
                2, -1, -7,
                6, -1, 5);

            Assert.True(m.Cofactor(0, 0).DoubleEqual(-12));
            Assert.True(m.Cofactor(1, 0).DoubleEqual(-25));
        }

        [Fact]
        public void DeterminantTest3x3()
        {
            Matrix m = new Matrix(1, 2, 6,
                -5, 8, -4,
                2, 6, 4);

            Assert.True(m.Cofactor(0, 0).DoubleEqual(56));
            Assert.True(m.Cofactor(0, 1).DoubleEqual(12));
            Assert.True(m.Cofactor(0, 2).DoubleEqual(-46));
            Assert.True(m.Determinant().DoubleEqual(-196));
        }


        [Fact]
        public void DeterminantTest4x4()
        {
            Matrix m = new Matrix(-2, -8, 3, 5,
                -3, 1, 7, 3,
                1, 2, -9, 6,
                -6, 7, 7, -9);

            Assert.True(m.Cofactor(0, 0).DoubleEqual(690));
            Assert.True(m.Cofactor(0, 1).DoubleEqual(447));
            Assert.True(m.Cofactor(0, 2).DoubleEqual(210));
            Assert.True(m.Cofactor(0, 3).DoubleEqual(51));
            Assert.True(m.Determinant().DoubleEqual(-4071));
        }

        [Fact]
        public void Invertabe()
        {
            Matrix m = new Matrix(6, 4, 4, 4,
                5, 5, 7, 6,
                4, -9, 3, 7,
                9, 1, 7, -6);

            Assert.True(m.IsInvertable());
        }

        [Fact]
        public void NotInvertabe()
        {
            Matrix m = new Matrix(-4, 2, -2, 3,
                9, 6, 2, 6,
                0, -5, 1, -5,
                0, 0, 0, 0);

            Assert.True(!m.IsInvertable());
        }

        [Fact]
        public void InverseOne()
        {
            Matrix m = new Matrix(-5, 2, 6, -8,
                1, -5, 1, 8,
                7, 7, -6, -7,
                1, -3, 7, 4);
            Matrix i = m.Inverse();

            Matrix result = new Matrix(0.21805, 0.45113, 0.24060, -0.04511,
                 -0.80827, -1.45677, -0.44361, 0.52068,
                 -0.07895, -0.22368, -0.05263, 0.19737,
                 -0.52256, -0.81391, -0.30075, 0.30639);

            Assert.True(result == i);
        }

        [Fact]
        public void InverseTwo()
        {
            Matrix m = new Matrix(8, -5, 9, 2,
7, 5, 6, 1,
-6, 0, 9, 6,
-3, 0, -9, -4
);
            Matrix i = m.Inverse();

            Matrix result = new Matrix(-0.15385, -0.15385, -0.28205, -0.53846,
-0.07692, 0.12308, 0.02564, 0.03077,
0.35897, 0.35897, 0.43590, 0.92308,
-0.69231, -0.69231, -0.76923, -1.92308
);

            Assert.True(result == i);
        }

        [Fact]
        public void InverseThree()
        {
            Matrix m = new Matrix(9, 3, 0, 9,
 -5, -2, -6, -3,
 -4, 9, 6, 4,
 -7, 6, 6, 2);
            Matrix i = m.Inverse();

            Matrix result = new Matrix(-0.04074, -0.07778, 0.14444, -0.22222,
 -0.07778, 0.03333, 0.36667, -0.33333,
 -0.02901, -0.14630, -0.10926, 0.12963,
 0.17778, 0.06667, -0.26667, 0.33333);

            Assert.True(result == i);
        }

        [Fact]
        public void InverseUsage()
        {
            Matrix a = new Matrix(3, -9, 7, 3,
 3, -8, 2, -9,
 -4, 4, 4, 1,
 -6, 5, -1, 1);
            Matrix b = new Matrix(8, 2, 2, 2,
 3, -1, 7, 0,
 7, 0, 5, 4,
 6, -2, 0, 5
);
            var c = a * b;
            Assert.True(c * b.Inverse() == a);

        }

        [Fact]
        public void TransformationMatrixForDefaultOrientation()
        {
            var t = Matrix.ViewTransform(Tuple.Point(0, 0, 0), Tuple.Point(0, 0, -1), Tuple.Vector(0, 1, 0));
            t.Should().Be(Matrix.Identity());
        }

        [Fact]
        public void TransformationMatrixLookingPositiveZDirection()
        {
            var t = Matrix.ViewTransform(Tuple.Point(0, 0, 0), Tuple.Point(0, 0, 1), Tuple.Vector(0, 1, 0));
            t.Should().Be(Matrix.Identity().Scaling(-1, 1, -1).Apply());
        }

        [Fact]
        public void ViewTransformationMovesTheWorld()
        {
            var t = Matrix.ViewTransform(Tuple.Point(0, 0, 8), Tuple.Point(0, 0, 0), Tuple.Vector(0, 1, 0));
            var transformation = Matrix.Identity().Translation(0, 0, -8).Apply();
            t.Should().Be(transformation);
        }

        [Fact]
        public void ArbitraryViewTransformation()
        {
            var t = Matrix.ViewTransform(Tuple.Point(1, 3, 2), Tuple.Point(4, -2, 8), Tuple.Vector(1, 1, 0));
            var result = new Matrix(-0.50709, 0.50709, 0.67612, -2.36643,
 0.76772, 0.60609, 0.12122, -2.82843,
 -0.35857, 0.59761, -0.71714, 0.00000,
 0.00000, 0.00000, 0.00000, 1.00000);
            t.Should().Be(result);
        }
    }
}
