using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class SphereTests
    {
        [Fact]
        public void RayIntersectAtTwoPoints()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            var xs = sphere.Intersect(ray);

            Assert.True(xs.Count == 2);
            Assert.True(xs[0].T.DoubleEqual(4.0));
            Assert.True(xs[1].T.DoubleEqual(6.0));
        }

        [Fact]
        public void RayIntersectAtATangent()
        {
            var ray = new Ray(Tuple.Point(0, 1, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            var xs = sphere.Intersect(ray);

            Assert.True(xs.Count == 2);
            Assert.True(xs[0].T.DoubleEqual(5.0));
            Assert.True(xs[1].T.DoubleEqual(5.0));
        }

        [Fact]
        public void RayMissesSphere()
        {
            var ray = new Ray(Tuple.Point(0, 2, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            var xs = sphere.Intersect(ray);
            Assert.True(xs.Count == 0);
        }

        [Fact]
        public void RayOriginatesInside()
        {
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            var xs = sphere.Intersect(ray);

            Assert.True(xs.Count == 2);
            Assert.True(xs[0].T.DoubleEqual(-1.0));
            Assert.True(xs[1].T.DoubleEqual(1.0));
        }

        [Fact]
        public void SphereBehindRay()
        {
            var ray = new Ray(Tuple.Point(0, 0, 5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            var xs = sphere.Intersect(ray);

            Assert.True(xs.Count == 2);
            Assert.True(xs[0].T.DoubleEqual(-6.0));
            Assert.True(xs[1].T.DoubleEqual(-4.0));
        }
    }
}
