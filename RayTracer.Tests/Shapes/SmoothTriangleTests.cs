using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class SmoothTriangleTests
    {
        [Fact]
        public void ConstructingASmoothTriangleTest()
        {
            var p1 = Tuple.Point(0, 1, 0);
            var p2 = Tuple.Point(-1, 0, 0);
            var p3 = Tuple.Point(1, 0, 0);
            var n1 = Tuple.Point(0, 1, 0);
            var n2 = Tuple.Point(-1, 0, 0);
            var n3 = Tuple.Point(1, 0, 0);

            var t = new SmoothTriangle(p1, p2, p3,n1, n2, n3);

            t.P1.Should().Be(p1);
            t.P2.Should().Be(p2);
            t.P3.Should().Be(p3);
            t.N1.Should().Be(n1);
            t.N2.Should().Be(n2);
            t.N3.Should().Be(n3);
        }


        [Fact]
        public void IntersectionCanEncapsulateUAndVTest()
        {
            var s = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0),
                Tuple.Point(1, 0, 0));
            var i = new Intersection(s,3.5, 0.2, 0.4);
            i.U.Should().Be(0.2);
            i.V.Should().Be(0.4);
        }
    }
}
