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
            Matrix i1 = Matrix.Identity;
            Matrix i2 = Matrix.Identity;

            Assert.True(i1.Transpose() == i2);
        }
    }
}
