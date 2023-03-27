using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class CubeTests
    {
        [Theory]
        [InlineData(-5, 0.5, 0, 1, 0, 0, 4, 6)]
        [InlineData(0.5, 5, 0, 0, -1, 0, 4, 6)]
        [InlineData(0.5, -5, 0, 0, 1, 0, 4, 6)]
        [InlineData(0.5, 0, 5, 0, 0, -1, 4, 6)]
        [InlineData(0.5, 0, -5, 0, 0, 1, 4, 6)]
        [InlineData(0, 0.5, 0, 0, 0, 1, -1, 1)]
        public void RayIntersectsCube(double ox, double oy, double oz,
            double vx, double vy, double vz, int t1, int t2)
        {
            Cube c = new Cube();
            Ray r = new Ray(Tuple.Point(ox, oy, oz), Tuple.Vector(vx, vy, vz));
            var origin = r.Origin;
            var direction = r.Direction;
            var xs = new Intersections();
            c.Intersect(ref origin,ref direction, xs);
            xs[0].T.Should().Be(t1);
            xs[1].T.Should().Be(t2);
        }

        [Theory]
        [InlineData(-2, 0, 0, 0.2673, 0.5345, 0.8018)]
        [InlineData(0, -2, 0, 0.8018, 0.2673, 0.5345)]
        [InlineData(0, 0, -2, 0.5345, 0.8018, 0.2673)]
        [InlineData(2, 0, 2, 0, 0, -1)]
        [InlineData(0, 2, 2, 0, -1, 0)]
        [InlineData(2, 2, 0, -1, 0, 0)]
        public void RayMissesCube(double ox, double oy, double oz,
           double vx, double vy, double vz)
        {
            Cube c = new Cube();
            Ray r = new Ray(Tuple.Point(ox, oy, oz), Tuple.Vector(vx, vy, vz));
            var origin = r.Origin;
            var direction = r.Direction;
            var xs = new Intersections();
            c.Intersect(ref origin, ref direction, xs);
            xs.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1, 0.5, -0.8, 1, 0, 0)]
        [InlineData(-1, -0.2, 0.9, -1, 0, 0)]
        [InlineData(-0.4, 1, -0.1, 0, 1, 0)]
        [InlineData(0.3, -1, -0.7, 0, -1, 0)]
        [InlineData(-0.6, 0.3, 1, 0, 0, 1)]
        [InlineData(0.4, 0.4, -1, 0, 0, -1)]
        [InlineData(1, 1, 1, 1, 0, 0)]
        [InlineData(-1, -1, -1, -1, 0, 0)]

        public void NormalOfCube(double px, double py, double pz,
           double vx, double vy, double vz)
        {
            Cube c = new Cube();
            var p = Tuple.Point(px, py, pz);
            c.NormalAtLocal(p).Should().Be(Tuple.Vector(vx, vy, vz));
        }
    }
}
