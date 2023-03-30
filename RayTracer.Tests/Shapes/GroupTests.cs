using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Shapes
{
    public class GroupTests
    {
        [Fact]
        public void IntersectingRayWithAnEmptyGroupTest()
        {
            var g = new Group();
            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var xs = new Intersections();
            g.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Count.Should().Be(0);
        }
        [Fact]
        public void IntersectingRayWithNonEmptyGroupTest()
        {
            var g = new Group();
            var s1 = new Sphere();
            var s2 = new Sphere().Translate(tz: -3);
            var s3 = new Sphere().Translate(tx: 5);
            g.Add(s1, s2, s3);

            var r = Helper.Ray(0, 0, -5, 0, 0, 1);
            var xs = new Intersections();
            g.IntersectLocal(ref r.Origin, ref r.Direction, xs);
            xs.Sort();

            xs.Count.Should().Be(4);
            xs[0].Object.Should().BeSameAs(s2);
            xs[1].Object.Should().BeSameAs(s2);
            xs[2].Object.Should().BeSameAs(s1);
            xs[3].Object.Should().BeSameAs(s1);
        }

        [Fact]
        public void IntersectingTransformedGroupTest()
        {
            Group g = new Group();
            g.Scale(2);
            var s = new Sphere().Translate(tx: 5);
            g.Add(s);
            var r = Helper.Ray(10, 0, -10, 0, 0, 1);
            var xs = new Intersections();
            g.Intersect(ref r.Origin, ref r.Direction, xs);
            xs.Count.Should().Be(2);
        }

        [Fact]
        public void ConvertingPointFromWorldToObjectSpaceTest()
        {
            var g1 = new Group().Rotate(ry: Math.PI / 2);
            var g2 = new Group().Scale(2);

            g1.Add(g2);

            var s = new Sphere().Translate(tx: 5);
            g2.Add(s);
            var p = s.WorldToObject(Tuple.Point(-2, 0, -10));
            p.Should().Be(Tuple.Point(0, 0, -1));
        }

        [Fact]
        public void ConvertingNormalFromObjectToWorldSpaceTest()
        {
            var g1 = new Group().Rotate(ry: Math.PI / 2);
            var g2 = new Group().Scale(1, 2, 3);
            g1.Add(g2);
            var s = new Sphere().Translate(tx: 5);
            g2.Add(s);
            var n = s.NormalToWorld(Tuple.Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
            n.X.Should().BeApproximately(0.2857, 1e-4);
            n.Y.Should().BeApproximately(0.4286, 1e-4);
            n.Z.Should().BeApproximately(-0.8571, 1e-4);
        }

        [Fact]
        public void FindingTheNormalOnAChildObjectTest()
        {
            var g1 = new Group().Rotate(ry: Math.PI / 2);
            var g2 = new Group().Scale(1, 2, 3);
            g1.Add(g2);

            var s = new Sphere().Translate(tx: 5);
            g2.Add(s);
            var n = s.NormalAt(Tuple.Point(1.7321, 1.1547, -5.5774));
            n.X.Should().BeApproximately(0.2857, 1e-4);
            n.Y.Should().BeApproximately(0.4286, 1e-4);
            n.Z.Should().BeApproximately(-0.8571, 1e-4);
        }
    }
}
