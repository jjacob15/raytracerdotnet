using System;
using Xunit;

namespace RayTracer.Tests
{
    public class TupleTest
    {
        [Fact]
        public void TupleWithW1IsAPoint()
        {
            Tuple tuple = new Tuple(4.3, -4.2, 3.1, 1);
            Assert.Equal(tuple.X, 4.3d);
            Assert.Equal(tuple.Y, -4.2d);
            Assert.Equal(tuple.Z, 3.1d);
            Assert.Equal(tuple.W, 1);
            Assert.True(tuple.IsPoint);
            Assert.False(tuple.IsVector);
        }

        [Fact]
        public void TupleWithW1IsAVector()
        {
            Tuple tuple = new Tuple(4.3, -4.2, 3.1, 0);
            Assert.Equal(tuple.X, 4.3d);
            Assert.Equal(tuple.Y, -4.2d);
            Assert.Equal(tuple.Z, 3.1d);
            Assert.Equal(tuple.W, 0);
            Assert.True(tuple.IsVector);
            Assert.False(tuple.IsPoint);
        }

        [Fact]
        public void PointEqualityTest()
        {
            Tuple tuple = new Tuple(4.3, -4.2, 3.1, 1);
            Tuple point = Tuple.Point(4.3, -4.2, 3.1);
            Assert.Equal(tuple, point);
        }

        [Fact]
        public void VectorEqualityTest()
        {
            Tuple tuple = new Tuple(4.3, -4.2, 3.1, 0);
            Tuple vector = Tuple.Vector(4.3, -4.2, 3.1);
            Assert.Equal(tuple, vector);
        }

        [Fact]
        public void TupleAndPointAddition()
        {
            Tuple a1 = new Tuple(3, -2, 5, 1);
            Tuple a2 = new Tuple(-2, 3, 1, 0);
            Assert.Equal(a1 + a2, new Tuple(1, 1, 6, 1));
        }

        [Fact]
        public void TupleAddition()
        {
            Tuple a1 = new Tuple(3, -2, 5, 1);
            Tuple a2 = new Tuple(-2, 3, 1, 1);
            Assert.Equal(a1 + a2, new Tuple(1, 1, 6, 1));
        }

        [Fact]
        public void PointSubtraction()
        {
            Tuple a1 = Tuple.Point(3, 2, 1);
            Tuple a2 = Tuple.Point(5, 6, 7);
            Assert.Equal(a1 - a2, Tuple.Vector(-2, -4, -6));
        }

        [Fact]
        public void PointAndVectorSubtraction()
        {
            Tuple a1 = Tuple.Point(3, 2, 1);
            Tuple a2 = Tuple.Vector(5, 6, 7);
            Assert.Equal(a1 - a2, Tuple.Point(-2, -4, -6));
        }

        [Fact]
        public void Negation()
        {
            Tuple a1 = new Tuple(1, -2, 3, -4);
            Tuple a2 = new Tuple(-1, 2, -3, 4);
            Assert.Equal(--a1, a2);
        }

        [Fact]
        public void ScalarMultiplication()
        {
            Tuple a1 = new Tuple(1, -2, 3, -4);
            Tuple a2 = new Tuple(3.5, -7, 10.5, -14);
            var r = a1 * 3.5;
            Assert.Equal(r, a2);
        }

        [Fact]
        public void ScalarDivision()
        {
            Tuple a1 = new Tuple(1, -2, 3, -4);
            Tuple a2 = new Tuple(.5, -1, 1.5, -2);
            Assert.Equal(a1 / 2, a2);
        }


        [Fact]
        public void MagnitudeOne()
        {
            Tuple v = Tuple.Vector(0, 1, 0);
            Assert.Equal(v.Magnitude(), 1);
        }

        [Fact]
        public void MagnitudeTwo()
        {
            Tuple v = Tuple.Vector(0, 0, 1);
            Assert.Equal(v.Magnitude(), 1);
        }

        [Fact]
        public void MagnitudeThree()
        {
            Tuple v = Tuple.Vector(1, 2, 3);
            Assert.Equal(v.Magnitude(), Math.Sqrt(14));
        }

        [Fact]
        public void MagnitudeFour()
        {
            Tuple v = Tuple.Vector(-1, -2, -3);
            Assert.Equal(v.Magnitude(), Math.Sqrt(14));
        }

        [Fact]
        public void NormalizeOne()
        {
            Tuple v = Tuple.Vector(4, 0, 0);
            Assert.Equal(v.Normalize(v), Tuple.Vector(1, 0, 0));
        }

        [Fact]
        public void NormalizeTwo()
        {
            Tuple v = Tuple.Vector(1, 2, 3);
            Assert.Equal(v.Normalize(v), Tuple.Vector(0.26726, 0.53452, 0.80178));
        }

        [Fact]
        public void DotProduct()
        {
            Tuple v1 = Tuple.Vector(1, 2, 3);
            Tuple v2 = Tuple.Vector(2, 3, 4);
            Assert.Equal(v1.Dot(v2), 20);
        }

        [Fact]
        public void CrossProduct()
        {
            Tuple v1 = Tuple.Vector(1, 2, 3);
            Tuple v2 = Tuple.Vector(2, 3, 4);
            Assert.Equal(v1.Cross(v2), Tuple.Vector(-1, 2, -1));
            Assert.Equal(v2.Cross(v1), Tuple.Vector(1, -2, 1));
        }
    }
}
