using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class CylinderTests
    {
        [Theory]
        [InlineData(1, 0, 0, 0, 1, 0)]
        [InlineData(0, 0, 0, 0, 1, 0)]
        [InlineData(0, 0, -5, 1, 1, 1)]
        public void RayMissesACylinder(double px, double py, double pz,
            double vx, double vy, double vz)
        {
            var c = new Cylinder();
            var origin = Tuple.Point(px, py, pz);
            var direction = Tuple.Vector(vx, vy, vz).Normalize();
            var xs = new Intersections();
            c.IntersectLocal(ref origin, ref direction, xs);
            xs.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1, 0, -5, 0, 0, 1, 5, 5)]
        [InlineData(0, 0, -5, 0, 0, 1, 4, 6)]
        [InlineData(0.5, 0, -5, 0.1, 1, 1, 6.80798, 7.08872)]
        public void RayStrikeACylinder(double px, double py, double pz,
           double vx, double vy, double vz, double t0, double t1)
        {
            var c = new Cylinder();
            var origin = Tuple.Point(px, py, pz);
            var direction = Tuple.Vector(vx, vy, vz).Normalize();
            var xs = new Intersections();
            c.IntersectLocal(ref origin, ref direction, xs);
            xs.Count.Should().Be(2);

            xs[0].T.Should().BeApproximately(t0, Constants.Epsilon);
            xs[1].T.Should().BeApproximately(t1, Constants.Epsilon);
        }
        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0)]
        [InlineData(0, 5, -1, 0, 0, -1)]
        [InlineData(0, -2, 1, 0, 0, 1)]
        [InlineData(-1, 1, 0, -1, 0, 0)]
        public void NormalOnACylinder(double px, double py, double pz,
           double vx, double vy, double vz)
        {
            var c = new Cylinder();
            var origin = Tuple.Point(px, py, pz);
            c.NormalAtLocal(origin).Should().Be(Tuple.Vector(vx, vy, vz));
        }
    }
}
