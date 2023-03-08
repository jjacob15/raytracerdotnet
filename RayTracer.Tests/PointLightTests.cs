using RayTracer.Lights;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class PointLightTests
    {
        [Fact]
        public void PointLightTest()
        {
            var intensity = new Color(0.1, 0.2, 0.3);
            var p = Tuple.Point(1, 2, 3);

            var light = new PointLight(p, intensity);
            Assert.Equal(light.Intensity ,intensity);
            Assert.Equal(light.Position ,p);
        }
    }
}
