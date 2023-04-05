using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class CsgTests
    {
        [Theory]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, false, false, true)]
        [InlineData(false, true, true, false)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, true)]
        [InlineData(false, false, false, true)]
        public void Union_IntersectionAllowedTest(bool leftHit, bool insideLeft, bool insideRight, bool expected)
        {
            var unionCsg = new Csg(CsgOperation.Union, new Sphere(), new Sphere());
            unionCsg.IntersectionAllowed(leftHit, insideLeft, insideRight).Should().Be(expected);
        }

        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(true, true, false, false)]
        [InlineData(true, false, true, true)]
        [InlineData(true, false, false, false)]
        [InlineData(false, true, true, true)]
        [InlineData(false, true, false, true)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, false)]
        public void IntersectAllowedTest(bool leftHit, bool insideLeft, bool insideRight, bool expected)
        {
            var intersectCsg = new Csg(CsgOperation.Intersect, new Sphere(), new Sphere());
            intersectCsg.IntersectionAllowed(leftHit, insideLeft, insideRight).Should().Be(expected);
        }

        [Theory]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, false, false, true)]
        [InlineData(false, true, true, true)]
        [InlineData(false, true, false, true)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, false)]
        public void DifferenceAllowedTest(bool leftHit, bool insideLeft, bool insideRight, bool expected)
        {
            var diffCsg = new Csg(CsgOperation.Difference, new Sphere(), new Sphere());
            diffCsg.IntersectionAllowed(leftHit, insideLeft, insideRight).Should().Be(expected);
        }

        private IShape s1 = new Sphere();
        private IShape s2 = new Cube();

        [Theory]
        [InlineData(CsgOperation.Union, 0, 3)]
        [InlineData(CsgOperation.Intersect, 1, 2)]
        [InlineData(CsgOperation.Difference, 0, 1)]
        public void FilteringUnionTest(CsgOperation op, int x0, int x1)
        {
            var csg = new Csg(op, s1, s2);
            var xs = new Intersections(new Intersection(s1, 1), new Intersection(s2, 2), new Intersection(s1, 3), new Intersection(s2, 4));
            var result = csg.Filter(xs);
            result.Count.Should().Be(2);
            result[0].Should().Be(xs[x0]);
            result[1].Should().Be(xs[x1]);
        }

        [Fact]
        public void RayMissesCsgObjectTest()
        {
            var c = new Csg(CsgOperation.Union, new Sphere(), new Cube());
            var r = Helper.Ray(0, 2, -5, 0, 0, 1);
            var xs = new Intersections();
            c.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Should().BeEmpty();
        } 
        [Fact]
        public void RayHitsCsgObjectTest()
        {
            var sphere1 = new Sphere();
            var sphere2 = new Sphere();
            var csg = new Csg(CsgOperation.Union, sphere1, sphere2);
            sphere2.Translate(0, 0, 0.5);
            
            var r = Helper.Ray(0, 0, -5, 0, 0, 1);
            var xs = new Intersections();
            csg.IntersectLocal(ref r.Origin, ref r.Direction, xs);

            xs.Count.Should().Be(2);
            xs[0].T.Should().Be(4);
            xs[0].Object.Should().Be(sphere1);
            xs[1].T.Should().Be(6.5);
            xs[1].Object.Should().Be(sphere2);
        }
    }
}
