using RayTracer.Lights;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class MaterialTests
    {
        public void LightingBetweenEyeAndSurface()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(light, position, eyev, normalv);
            Assert.Equal(new Color(1.9, 1.9, 1.9), result);
        }

        public void LightingBetweenEyeAndSurfaceOffset45()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(light, position, eyev, normalv);
            Assert.Equal(new Color(1.0, 1.0, 1.0), result);
        }

        public void LightingBetweenEyeLightAndSurfaceOffset45()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(light, position, eyev, normalv);
            Assert.Equal(new Color(0.7364, 0.7364, 0.7364), result);
        }


        public void LightingInPathOfReflectionVector()
        {
            var position = Tuple.Point(0, 0, 0);
            var eyev = Tuple.Vector(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 10, -10), new Color(1, 1, 1));
            var result = new Material().Lighting(light, position, eyev, normalv);
            Assert.Equal(new Color(1.6364, 1.6364, 1.6364), result);
        }

        public void LightingWithLightBehindSurface()
        {
            var position = Tuple.Point(0, 0, -1);
            var eyev = Tuple.Vector(0, 0, -1);
            var normalv = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, 10), new Color(1, 1, 1));
            var result = new Material().Lighting(light, position, eyev, normalv);
            Assert.Equal(new Color(0.1, 0.1, 0.1), result);
        }
    }
}
