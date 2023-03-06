using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class ShearingTests
    {
        [Fact]
        public void ShearingXY()
        {
            Matrix m = Matrix.Identity().Shearing(1, 0, 0, 0, 0, 0).Apply();
            Tuple p = Tuple.Point(2, 3, 4);
            Assert.Equal(m * p, Tuple.Point(5, 3, 4));
        }

        [Fact]
        public void ShearingXZ()
        {
            Matrix m = Matrix.Identity().Shearing(0, 1, 0, 0, 0, 0).Apply();
            Tuple p = Tuple.Point(2, 3, 4);
            Assert.Equal(m * p, Tuple.Point(6, 3, 4));
        }

        [Fact]
        public void ShearingYX()
        {
            Matrix m = Matrix.Identity().Shearing(0, 0, 1, 0, 0, 0).Apply();
            Tuple p = Tuple.Point(2, 3, 4);
            Assert.Equal(m * p, Tuple.Point(2,5,4));
        }

        [Fact]
        public void ShearingYZ()
        {
            Matrix m = Matrix.Identity().Shearing(0, 0, 0, 1, 0, 0).Apply();
            Tuple p = Tuple.Point(2, 3, 4);
            Assert.Equal(m * p, Tuple.Point(2, 7, 4));
        }

        [Fact]
        public void ShearingZX()
        {
            Matrix m = Matrix.Identity().Shearing(0, 0, 0, 0, 1, 0).Apply();
            Tuple p = Tuple.Point(2, 3, 4);
            Assert.Equal(m * p, Tuple.Point(2, 3, 6));
        }

        [Fact]
        public void ShearingZY()
        {
            Matrix m = Matrix.Identity().Shearing(0, 0, 0, 0, 0, 1).Apply();
            Tuple p = Tuple.Point(2, 3, 4);
            Assert.Equal(m * p, Tuple.Point(2,3,7));
        }
    }
}
