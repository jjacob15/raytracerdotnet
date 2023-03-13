using FluentAssertions;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace RayTracer.Shapes
{
    public class PlaneTests
    {
        [Fact]
        public void BasicPlaneTest()
        {
            var p = new Plane();
            var n1 = p.NormalAtLocal(Tuple.Point(0, 0, 0));
            var n2 = p.NormalAtLocal(Tuple.Point(10, 0, -10));
            var n3 = p.NormalAtLocal(Tuple.Point(-5, 0, 150));

            n1.Should().Be(Tuple.Vector(0, 1, 0));
            n2.Should().Be(Tuple.Vector(0, 1, 0));
            n3.Should().Be(Tuple.Vector(0, 1, 0));
        }

        [Fact]
        public void IntersectWithRayParallelToPlane()
        {
            var p = new Plane();
            var r = new Ray(Tuple.Point(0, 10, 0), Tuple.Vector(0, 0, 1));
            var xs = p.IntersectLocal(r);
            xs.Hit().Should().Be(null);
        }

        [Fact]
        public void IntersectWithCoplanarRay()
        {
            var p = new Plane();
            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var xs = p.IntersectLocal(r);
            xs.Hit().Should().Be(null);
        }

        [Fact]
        public void RayFromAbove()
        {
            var p = new Plane();
            var r = new Ray(Tuple.Point(0, 1, 0), Tuple.Vector(0, -1, 0));
            var xs = p.IntersectLocal(r);
            xs.Count.Should().Be(1);
            xs[0].T.Should().Be(1);
            xs[0].Object.Should().BeOfType<Plane>();
            xs[0].Object.Should().Be(p);
        }

        [Fact]
        public void RayFromBelow()
        {
            var p = new Plane();
            var r = new Ray(Tuple.Point(0, -1, 0), Tuple.Vector(0, 1, 0));
            var xs = p.IntersectLocal(r);
            xs.Count.Should().Be(1);
            xs[0].T.Should().Be(1);
            xs[0].Object.Should().BeOfType<Plane>();
            xs[0].Object.Should().Be(p);
        }
    }
}
