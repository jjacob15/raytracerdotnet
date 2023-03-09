using FluentAssertions;
using RayTracer.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class IntersectionTests
    {
        [Fact]
        public void IntersectionEncapsulateT()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(ray);

            Assert.Equal(2, xs.Count);
            Assert.Equal(s, xs[0].Object);
            Assert.Equal(s, xs[1].Object);
        }

        [Fact]
        public void HitWithPositiveT()
        {
            var s = new Sphere();
            var i1 = s.Intersection(1);
            var i2 = s.Intersection(2);

            var xs = new Intersections(i1, i2);
            var hit = xs.Hit();

            Assert.Equal(hit, i1);
        }

        [Fact]
        public void HitWithNegT()
        {
            var s = new Sphere();
            var i1 = s.Intersection(-1);
            var i2 = s.Intersection(1);

            var xs = new Intersections(i1, i2);
            var hit = xs.Hit();

            Assert.Equal(hit, i2);
        }

        [Fact]
        public void HitLowesthNegT()
        {
            var s = new Sphere();
            var i1 = s.Intersection(5);
            var i2 = s.Intersection(7);
            var i3 = s.Intersection(-3);
            var i4 = s.Intersection(2);

            var xs = new Intersections(i1, i2, i3, i4);
            var hit = xs.Hit();

            Assert.Equal(hit, i4);
        }

        [Fact]
        public void IntersectingScaledSphereWithARay()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            s.Transform = Matrix.Identity().Scaling(2, 2, 2).Apply();

            var xs = s.Intersect(ray);

            Assert.Equal(2, xs.Count);
            Assert.True(xs[0].T.DoubleEqual(3));
            Assert.True(xs[1].T.DoubleEqual(7));
        }


        [Fact]
        public void IntersectingTranslatedSphereWithARay()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            s.Transform = Matrix.Identity().Translation(5, 0, 0).Apply();

            var xs = s.Intersect(ray);

            Assert.Equal(0, xs.Count);
        }

        [Fact]
        public void PrecomputeStateOfAnIntersection()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 4);
            var comps = i.PrepareComputations(r);

            comps.T.Should().Be(i.T);
            comps.Obj.Should().Be(i.Object);
            comps.Point.Should().Be(Tuple.Point(0, 0, -1));
            comps.EyeV.Should().Be(Tuple.Vector(0, 0, -1));
            comps.NormalV.Should().Be(Tuple.Vector(0, 0, -1));
        }

        [Fact]
        public void HitWhenIntersectionOccursOutside()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 4);
            var comp = i.PrepareComputations(r);

            comp.Inside.Should().Be(false);
        }

        [Fact]
        public void HitWhenIntersectionOccursInside()
        {
            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 1);

            var comp = i.PrepareComputations(r);
            comp.Point.Should().Be(Tuple.Point(0, 0, 1));
            comp.EyeV.Should().Be(Tuple.Vector(0, 0, -1));
            comp.Inside.Should().Be(true);
            comp.NormalV.Should().Be(Tuple.Vector(0, 0, -1));
        }

        [Fact]
        public void ShadingAnIntersection()
        {
            var w = new World();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var shape = w.Shapes.First();
            var i = new Intersection(shape, 4);
            var comps = i.PrepareComputations(r);
            w.ShadeHits(comps).Should().Be(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void ShadingAnIntersectionFromInside()
        {
            var w = new World();
            w.Light = new PointLight(Tuple.Point(0, 0.25, 0), new Color(1, 1, 1));

            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var shape = w.Shapes[1];
            var i = new Intersection(shape, 0.5);
            var comps = i.PrepareComputations(r);
            w.ShadeHits(comps).Should().Be(new Color(0.90498, 0.90498, 0.90498));
        }

        [Fact]
        public void ColorWhenRayMisses()
        {
            var w = new World();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 1, 0));
            w.ColorAt(r).Should().Be(Color.Black);
        }

        [Fact]
        public void ColorWhenRayHits()
        {
            var w = new World();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            w.ColorAt(r).Should().Be(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void ColorWhenIntersectionBehindRay()
        {
            var w = new World();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            w.ColorAt(r).Should().Be(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void ColorWithIntersectionBehindTheRay()
        {
            var w = new World();
            var outer = w.Shapes.First();
            outer.Material.Ambient = 1;

            var inner = w.Shapes[1];
            inner.Material.Ambient = 1;
            var r = new Ray(Tuple.Point(0, 0, .75), Tuple.Vector(0, 0, -1));
            w.ColorAt(r).Should().Be(inner.Material.Color);
        }
    }
}
