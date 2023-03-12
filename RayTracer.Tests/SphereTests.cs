using RayTracer.Shapes;
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

        [Fact]
        public void NormalOfSphereOnX()
        {
            var sphere = new Sphere();
            var normal = sphere.NormalAt(Tuple.Point(1, 0, 0));

            Assert.True(normal == Tuple.Vector(1, 0, 0));
        }
        [Fact]
        public void NormalOfSphereOnY()
        {
            var sphere = new Sphere();
            var normal = sphere.NormalAt(Tuple.Point(0, 1, 0));

            Assert.True(normal == Tuple.Vector(0, 1, 0));
        }

        [Fact]
        public void NormalOfSphereOnZ()
        {
            var sphere = new Sphere();
            var normal = sphere.NormalAt(Tuple.Point(0, 0, 1));

            Assert.True(normal == Tuple.Vector(0, 0, 1));
        }

        [Fact]
        public void NormalOfSphereOnNonaxialPoint()
        {
            var sphere = new Sphere();
            var n = Math.Sqrt(3) / 3;
            var normal = sphere.NormalAt(Tuple.Point(n, n, n));

            Assert.True(normal == Tuple.Vector(n, n, n));
            Assert.True(normal == Tuple.Vector(n, n, n).Normalize());
        }

        [Fact]
        public void NormalOfTranslatedSphere()
        {
            var sphere = new Sphere();
            sphere.Transform = Matrix.Identity().Translation(0, 1, 0).Apply();
            var normal = sphere.NormalAt(Tuple.Point(0, 1.70711, -0.70711));

            Assert.True(normal == Tuple.Vector(0, 0.70711, -0.70711));
        }


        [Fact]
        public void NormalOfTransformedSphere()
        {
            var sphere = new Sphere();
            sphere.Transform = Matrix.Identity().RotateZ(Math.PI / 5).Scaling(1, 0.5, 1).Apply();
            var normal = sphere.NormalAt(Tuple.Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));

            Assert.True(normal == Tuple.Vector(0, 0.97014, -0.24254));
        }

    }
}
