using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer
{
    public class RotationTests
    {
        [Fact]
        public void RotationX()
        {
            Tuple point = Tuple.Point(0, 1, 0);
            Matrix halfQuater = Matrix.Identity().RotateX(Math.PI / 4).Apply();
            Matrix fullQuater = Matrix.Identity().RotateX(Math.PI / 2).Apply();

            Assert.True(halfQuater * point == Tuple.Point(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            Assert.True(fullQuater * point == Tuple.Point(0, 0, 1));
        }

        [Fact]
        public void RotationXInverse()
        {
            Tuple point = Tuple.Point(0, 1, 0);
            Matrix halfQuater = Matrix.Identity().RotateX(Math.PI / 4).Apply();
            var inv = halfQuater.Inverse();

            Assert.True(inv * point == Tuple.Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
        }

        [Fact]
        public void RotationY()
        {
            Tuple point = Tuple.Point(0, 0, 1);
            Matrix halfQuater = Matrix.Identity().RotateY(Math.PI / 4).Apply();
            Matrix fullQuater = Matrix.Identity().RotateY(Math.PI / 2).Apply();

            Assert.True(halfQuater * point == Tuple.Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2));
            Assert.True(fullQuater * point == Tuple.Point(1, 0, 0));
        }

        [Fact]
        public void RotationZ()
        {
            Tuple point = Tuple.Point(0, 1, 0);
            Matrix halfQuater = Matrix.Identity().RotateZ(Math.PI / 4).Apply();
            Matrix fullQuater = Matrix.Identity().RotateZ(Math.PI / 2).Apply();

            Assert.True(halfQuater * point == Tuple.Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0));
            Assert.True(fullQuater * point == Tuple.Point(-1, 0, 0));
        }
    }
}
