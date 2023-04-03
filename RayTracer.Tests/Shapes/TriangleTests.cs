using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class TriangleTests
    {
        [Fact]
        public void ConstructingATriangleTest()
        {
            var p1 = Tuple.Point(0, 1, 0);
            var p2 = Tuple.Point(-1, 0, 0);
            var p3 = Tuple.Point(1, 0, 0);
            var t = new Triangle(p1, p2, p3);
            t.P1.Should().Be(p1);
            t.P2.Should().Be(p2);
            t.P3.Should().Be(p3);
            t.E1.Should().Be(Tuple.Vector(-1, -1, 0));
            t.E2.Should().Be(Tuple.Vector(1, -1, 0));
            t.N.Should().Be(Tuple.Vector(0, 0, -1));
        }


        [Fact]
        public void FindingTheNormalOnATriangleTest()
        {
            var t = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0));
            var n1 = t.NormalAtLocal(Tuple.Point(0, 0.5, 0));
            var n2 = t.NormalAtLocal(Tuple.Point(-0.5, 0.75, 0));
            var n3 = t.NormalAtLocal(Tuple.Point(0.5, 0.25, 0));
            n1.Should().Be(t.N);
            n2.Should().Be(t.N);
            n3.Should().Be(t.N);
        }

        [Fact]
        public void IntersectingARayParallelToTheTriangleTest()
        {
            var t = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0));
            var r = new Ray(Tuple.Point(0, -1, -2), Tuple.Vector(0, 1, 0));
            var xs = new Intersections();
            t.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Should().BeEmpty();
        }

        [Fact]
        public void ARayMissesTheP1P3EdgeTest()
        {
            var t = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0));
            var r = new Ray(Tuple.Point(1, 1, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            t.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Should().BeEmpty();
        }

        [Fact]
        public void ARayMissesTheP1P2EdgeTest()
        {
            var t = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0));
            var r = new Ray(Tuple.Point(-1, 1, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            t.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Should().BeEmpty();
        }

        [Fact]
        public void ARayMissesTheP2P3EdgeTest()
        {
            var t = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0));
            var r = new Ray(Tuple.Point(0, -1, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            t.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Should().BeEmpty();
        }

        [Fact]
        public void ARayStrikesATriangleTest()
        {
            var t = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0), Tuple.Point(1, 0, 0));
            var r = new Ray(Tuple.Point(0, 0.5, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            t.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Count.Should().Be(1);
            xs[0].T.Should().Be(2);
        }
    }
}
