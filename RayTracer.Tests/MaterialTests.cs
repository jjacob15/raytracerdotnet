using FluentAssertions;
using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class MaterialTests
    {
        [Fact]
        public void LightingBetweenEyeAndSurface()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(new Sphere(),light,ref position, ref eyev, ref normalv, false);
            Assert.Equal(new Color(1.9, 1.9, 1.9), result);
        }

        [Fact]
        public void LightingBetweenEyeAndSurfaceOffset45()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(new Sphere(), light,ref position, ref eyev, ref normalv, false);
            Assert.Equal(new Color(1.0, 1.0, 1.0), result);
        }

        [Fact]
        public void LightingBetweenEyeLightAndSurfaceOffset45()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 10, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(new Sphere(), light,ref position, ref eyev, ref normalv, false);
            Assert.Equal(new Color(0.7364, 0.7364, 0.7364), result);
        }

        [Fact]
        public void LightingInPathOfReflectionVector()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 10, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(new Sphere(), light,ref position, ref eyev, ref normalv, false);
            Assert.Equal(new Color(1.6364, 1.6364, 1.6364), result);
        }

        [Fact]
        public void LightingWithLightBehindSurface()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, 10), new Color(1, 1, 1));
            var result = new Material().Lighting(new Sphere(), light,ref position, ref eyev, ref normalv, false);
            Assert.Equal(new Color(0.1, 0.1, 0.1), result);
        }

        [Fact]
        public void LightingWithSurfaceInShadow()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(new Sphere(), light, ref position, ref eyev, ref normalv, true);
            result.Should().Be(new Color(0.1, 0.1, 0.1));
        }
    }
}
