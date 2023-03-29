using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class ConeTests
    {
        [Theory]
        [InlineData(0, 0, -5, 0, 1, 0, 0)]
        [InlineData(0, 0, -0.25, 0, 1, 1, 2)]
        [InlineData(0, 0, -0.25, 0, 1, 0, 4)]
        public void IntersectingConeEndCapsTest(double x, double y, double z, double dx, double dy, double dz, int c)
        {
            var shape = new Cone(-0.5, 0.5, true);
            var direction = Tuple.Vector(dx, dy, dz).Normalize();
            var origin = Tuple.Point(x, y, z);
            var xs = new Intersections();
            shape.IntersectLocal(ref origin, ref direction, xs);
            xs.Count.Should().Be(c);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(-1, -1, 0, -1, 1, 0)]
        public void ComputingNormalVectorTest(double x, double y, double z, double dx, double dy, double dz)
        {
            var shape = new Cone();
            var n = shape.NormalAtLocal(Tuple.Point(x, y, z));
            n.Should().Be(Tuple.Vector(dx, dy, dz));
        }

        [Fact]
        public void ComputingNormalVector_Negative_Y_Test()
        {
            ComputingNormalVectorTest(1, 1, 1, 1, -Math.Sqrt(2), 1);
        }
    }
}
