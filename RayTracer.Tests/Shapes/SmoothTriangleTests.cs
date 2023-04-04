using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class SmoothTriangleTests
    {
        private Tuple p1;
        private Tuple p2;
        private Tuple p3;
        private Tuple n1;
        private Tuple n2;
        private Tuple n3;
        private SmoothTriangle st;

        public SmoothTriangleTests()
        {
            p1 = Tuple.Point(0, 1, 0);
            p2 = Tuple.Point(-1, 0, 0);
            p3 = Tuple.Point(1, 0, 0);
            n1 = Tuple.Vector(0, 1, 0);
            n2 = Tuple.Vector(-1, 0, 0);
            n3 = Tuple.Vector(1, 0, 0);
            st = new SmoothTriangle(p1, p2, p3, n1, n2, n3);
        }
        [Fact]
        public void ConstructingASmoothTriangleTest()
        {
            st.P1.Should().Be(p1);
            st.P2.Should().Be(p2);
            st.P3.Should().Be(p3);
            st.N1.Should().Be(n1);
            st.N2.Should().Be(n2);
            st.N3.Should().Be(n3);
        }


        [Fact]
        public void IntersectionCanEncapsulateUAndVTest()
        {
            var s = new Triangle(Tuple.Point(0, 1, 0), Tuple.Point(-1, 0, 0),
                Tuple.Point(1, 0, 0));
            var i = new Intersection(s, 3.5, 0.2, 0.4);
            i.U.Should().Be(0.2);
            i.V.Should().Be(0.4);
        }

        [Fact]
        public void IntersectionWithSmoothTriangleStoreUV()
        {
            var r = new Ray(Tuple.Point(-0.2, .3, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            st.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs[0].U.Should().BeApproximately(0.45, Constants.Epsilon);
            xs[0].V.Should().BeApproximately(0.25, Constants.Epsilon);
        }


        [Fact]
        public void SmoothTriangleUsesUvToInterpolateTheNormal()
        {
            var i = new Intersection(st, 1, 0.45, 0.25);

            var n = st.NormalAt(Tuple.Point(0, 0, 0), i);
            n.X.Should().BeApproximately(-0.5547, Constants.Epsilon);
            n.Y.Should().BeApproximately(0.83205, Constants.Epsilon);
            n.Z.Should().BeApproximately(0, Constants.Epsilon);
        }

        [Fact]
        public void PerpareNormalOnSmoothTriangle()
        {
            var i = new Intersection(st, 1, 0.45, 0.25);
            var r = new Ray(Tuple.Point(-0.2, .3, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            xs.Add(i);
            var comps = i.PrepareComputations(r, xs);
            comps.NormalV.X.Should().BeApproximately(-0.5547, Constants.Epsilon);
            comps.NormalV.Y.Should().BeApproximately(0.83205, Constants.Epsilon);
            comps.NormalV.Z.Should().BeApproximately(0, Constants.Epsilon);
        }
    }
}
