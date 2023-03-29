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

        [Fact]
        public void CylinderMinMax()
        {
            var c = new Cylinder();
            c.Minimum.Should().Be(double.NegativeInfinity);
            c.Maximum.Should().Be(double.PositiveInfinity);
        }


        [Theory]
        [InlineData(0, 1.5, 0, 0.1, 1, 0, 0)]
        [InlineData(0, 3, -5, 0, 0, 1, 0)]
        [InlineData(0, 0, -5, 0, 0, 1, 0)]
        [InlineData(0, 2, -5, 0, 0, 1, 0)]
        [InlineData(0, 1, -5, 0, 0, 1, 0)]
        [InlineData(0, 1.5, -2, 0, 0, 1, 2)]
        public void IntersectingConstraintCylinder(double px, double py, double pz,
           double vx, double vy, double vz, int count)
        {
            var c = new Cylinder();
            c.Minimum = 1;
            c.Maximum = 2;
            var direction = Tuple.Vector(vx, vy, vz).Normalize();
            var origin = Tuple.Point(px, py, pz);
            var xs = new Intersections();
            c.IntersectLocal(ref origin, ref direction, xs);
            xs.Count.Should().Be(count);
        }

        [Fact]
        public void ClosedValueCylinder()
        {
            var c = new Cylinder();
            c.Closed.Should().Be(false);
        }

        [Theory]
        [InlineData(0, 3, 0, 0, -1, 0, 2)]
        [InlineData(0, 3, -2, 0, -1, 2, 2)]
        [InlineData(0, 4, -2, 0, -1, 1, 2)]
        [InlineData(0, 0, -2, 0, 1, 2, 2)]
        [InlineData(0, -1, -2, 0, 1, 1, 2)]
        public void IntersectingTheCapsOfClosedCylinder(double px, double py, double pz,
          double vx, double vy, double vz, int count)
        {
            var c = new Cylinder();
            c.Minimum = 1;
            c.Maximum = 2;
            c.Closed = true;
            var direction = Tuple.Vector(vx, vy, vz).Normalize();
            var origin = Tuple.Point(px, py, pz);
            var xs = new Intersections();
            c.IntersectLocal(ref origin, ref direction, xs);
            xs.Count.Should().Be(count);
        }

        [Theory]
        [InlineData(0, 1, 0, 0, -1, 0)]
        [InlineData(0.5, 1, 0, 0, -1, 0)]
        [InlineData(0, 1, 0.5, 0, -1, 0)]
        [InlineData(0, 2, 0, 0, 1, 0)]
        [InlineData(0.5, 2, 0, 0, 1, 0)]
        [InlineData(0, 2, 0.5, 0, 1, 0)]
        public void NormalVectorOnEndCap(double px, double py, double pz,
          double vx, double vy, double vz)
        {
            var c = new Cylinder();
            c.Minimum = 1;
            c.Maximum = 2;
            c.Closed = true;
            c.NormalAtLocal(Tuple.Point(px, py, pz)).Should().Be(Tuple.Vector(vx, vy, vz));
        }
    }
}
