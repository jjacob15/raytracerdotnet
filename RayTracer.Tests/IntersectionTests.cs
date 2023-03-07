using System;
using System.Collections.Generic;
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
            s.SetTransform(Matrix.Identity().Scaling(2, 2, 2).Apply());

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
            s.SetTransform(Matrix.Identity().Translation(5, 0, 0).Apply());

            var xs = s.Intersect(ray);

            Assert.Equal(0, xs.Count);
        }
    }
}
