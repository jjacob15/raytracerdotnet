using FluentAssertions;
using RayTracer.Lights;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class WorldTests
    {
        [Fact]
        public void DefaultWorld()
        {
            Sphere s1 = new Sphere();
            s1.Material = new Material(new Color(0.8, 1.0, 0.6), 0.1, 0.7, 0.2, 200);
            Sphere s2 = new Sphere();
            s2.Transform = Matrix.Identity().Scaling(0.5, 0.5, 0.5).Apply();

            var world = World.DefaultWorld();
            world.Shapes[0].Should().Be(s1);
            world.Shapes[1].Should().Be(s2);

            world.Light.Should().Be(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));
        }

        [Fact]
        public void DefaultWorldIntersections()
        {
            var world = World.DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            var xs = world.Intersect(ray);
            xs.Count.Should().Be(4);
            xs[0].T.Should().Be(4);
            xs[1].T.Should().Be(4.5);
            xs[2].T.Should().Be(5.5);
            xs[3].T.Should().Be(6);
        }
    }
}
