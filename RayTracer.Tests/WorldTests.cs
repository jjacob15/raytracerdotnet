using FluentAssertions;
using RayTracer.Lights;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;

namespace RayTracer
{
    public class WorldTests
    {
        [Fact]
        public void DefaultWorld()
        {
            Sphere s1 = new Sphere();
            s1.Material = new Material(new Color(0.8, 1.0, 0.6), 0.1, 0.7, 0.2, 200);
            Sphere s2 = new Sphere();
            s2.Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Apply();

            var world = ObjectFactory.DefaultWorld();
            world.Shapes[0].Should().Be(s1);
            world.Shapes[1].Should().Be(s2);

            world.Light.Should().Be(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));
        }

        [Fact]
        public void DefaultWorldIntersections()
        {
            var world = ObjectFactory.DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));


            var xs = new Intersections();
            world.Intersect(ray, xs);
            xs.Count.Should().Be(4);
            xs[0].T.Should().Be(4);
            xs[1].T.Should().Be(4.5);
            xs[2].T.Should().Be(5.5);
            xs[3].T.Should().Be(6);
        }

        [Fact]
        public void NoShadowWhenNothingIsCollinearWithPointAndLight()
        {
            World w = ObjectFactory.DefaultWorld();
            var p = Tuple.Point(0, 10, 0);
            w.IsShadowed(p).Should().Be(false);
        }

        [Fact]
        public void ShadowWhenObjectBetweenPointAndLight()
        {
            World w = ObjectFactory.DefaultWorld();
            var p = Tuple.Point(10, -10, 10);
            w.IsShadowed(p).Should().Be(true);
        }

        [Fact]
        public void NoShadowWhenObjectBehindTheLight()
        {
            World w = ObjectFactory.DefaultWorld();
            var p = Tuple.Point(-20, 20, -20);
            w.IsShadowed(p).Should().Be(false);
        }

        [Fact]
        public void ShadowWhenObjectBetweenPoint()
        {
            World w = ObjectFactory.DefaultWorld();
            var p = Tuple.Point(-2, 2, -2);
            w.IsShadowed(p).Should().Be(false);
        }

        [Fact]
        public void ShadeHitWithIntersectionInShadow()
        {
            var w = new World();
            w.SetLight(new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1)));
            w.AddShape(new Sphere());
            var s2 = new Sphere();
            s2.Transform = Matrix.Transformation().Translation(0, 0, 10).Apply();
            w.AddShape(s2);
            var r = new Ray(Tuple.Point(0, 0, 5), Tuple.Vector(0, 0, 1));
            var i = s2.Intersection(4);
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHits(comps);
            c.Should().Be(new Color(0.1, 0.1, 0.1));
        }

        [Fact]
        public void RefractedColorWithOpaqueSurface()
        {
            var w = ObjectFactory.DefaultWorld();
            var s = w.Shapes[0];
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var xs = new Intersections(new Intersection(s, 4), new Intersection(s, 6));
            var comps = xs[0].PrepareComputations(r, xs);
            w.RefractedColor(comps, 5).Should().Be(Color.Black);
        }


        [Fact]
        public void RefractedColorAtMaxDepth()
        {
            var w = ObjectFactory.DefaultWorld();
            var s = w.Shapes[0];
            s.Material.Transparency = 1;
            s.Material.RefractiveIndex = 1.5;

            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var xs = new Intersections(new Intersection(s, 4), new Intersection(s, 6));
            var comps = xs[0].PrepareComputations(r, xs);
            w.RefractedColor(comps, 0).Should().Be(Color.Black);
        }

        [Fact]
        public void RefractedColorUnderTotalReflection()
        {
            var w = ObjectFactory.DefaultWorld();
            var s = w.Shapes[0];
            s.Material.Transparency = 1;
            s.Material.RefractiveIndex = 1.5;

            var r = new Ray(Tuple.Point(0, 0, Math.Sqrt(2) / 2), Tuple.Vector(0, 1, 0));
            var xs = new Intersections(new Intersection(s, -Math.Sqrt(2) / 2), new Intersection(s, Math.Sqrt(2) / 2));
            var comps = xs[1].PrepareComputations(r, xs);
            w.RefractedColor(comps, 5).Should().Be(Color.Black);
        }

        [Fact]
        public void RefractedColorWithARefractedRay()
        {
            var w = ObjectFactory.DefaultWorld();
            var a = w.Shapes[0];
            a.Material.Ambient = 1;
            a.Material.Pattern = new TestPattern();

            var b = w.Shapes[1];
            b.Material.Transparency = 1;
            b.Material.RefractiveIndex = 1.5;

            var r = new Ray(Tuple.Point(0, 0, 0.1), Tuple.Vector(0, 1, 0));
            var xs = new Intersections(new Intersection(a, -0.9899), new Intersection(b, -0.4899), new Intersection(b, 0.4899), new Intersection(a, 0.9899));
            var comps = xs[2].PrepareComputations(r, xs);
            var c = w.RefractedColor(comps, 5);
            c.Red.Should().Be(0);
            c.Green.Should().BeApproximately(0.99888, 1e-4);
            c.Blue.Should().BeApproximately(0.04725, 1e-4);
        }

        [Fact]
        public void ShadeHitWithTransparentMaterial()
        {
            var w = ObjectFactory.DefaultWorld();

            var floor = new Plane();
            floor.Transform = Matrix.Transformation().Translation(0, -1, 0).Apply();
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;
            w.AddShape(floor);

            var ball = new Sphere();
            ball.Material.Color = new Color(1, 0, 0);
            ball.Material.Ambient = 0.5;
            ball.Transform = Matrix.Transformation().Translation(0, -3.5, -0.5).Apply();
            w.AddShape(ball);

            var r = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));

            var xs = new Intersections(new Intersection(floor, Math.Sqrt(2)));

            var comps = xs[0].PrepareComputations(r, xs);
            w.ShadeHits(comps, 5).Should().Be(new Color(0.93642, 0.68642, 0.68642));
        }

        [Fact]
        public void ShadeHitWithReflectiveTransparentMaterial()
        {
            var sqrt2 = Math.Sqrt(2);
            var w = ObjectFactory.DefaultWorld();
            var r = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -sqrt2 / 2, sqrt2 / 2));
            var floor = new Plane();
            floor.Transform = Matrix.Transformation().Translation(0, -1, 0).Apply();
            floor.Material.Reflective = 0.5;
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;

            w.AddShape(floor);

            var ball = new Sphere();
            ball.Material.Color = new Color(1, 0, 0);
            ball.Material.Ambient = 0.5;
            ball.Transform = Matrix.Transformation().Translation(0, -3.5, -0.5).Apply();

            w.AddShape(ball);
            var xs = new Intersections(new Intersection(floor, sqrt2));
            var comps = xs[0].PrepareComputations(r, xs);
            w.ShadeHits(comps, 5).Should().Be(new Color(0.93391, 0.69643, 0.69243));

        }
    }
}
