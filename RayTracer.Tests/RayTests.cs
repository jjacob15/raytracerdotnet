using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class RayTests
    {
        [Fact]
        public void RayTest()
        {
            Ray r = new Ray(Tuple.Point(2, 3, 4), Tuple.Vector(1, 0, 0));
            
            Assert.Equal(r.Position(0), Tuple.Point(2, 3, 4));
            Assert.Equal(r.Position(1), Tuple.Point(3, 3, 4));
            Assert.Equal(r.Position(-1), Tuple.Point(1, 3, 4));
            Assert.Equal(r.Position(2.5), Tuple.Point(4.5, 3, 4));
        }

        [Fact]
        public void TranslatingRayTest()
        {
            Ray r = new Ray(Tuple.Point(1, 2, 3), Tuple.Vector(0, 1, 0));

            var r2 = r.Transform(Matrix.Identity().Translation(3, 4, 5).Apply());

            Assert.Equal(r2.Origin, Tuple.Point(4, 6, 8));
            Assert.Equal(r2.Direction, Tuple.Vector(0, 1, 0));
        }

        [Fact]
        public void ScalingRayTest()
        {
            Ray r = new Ray(Tuple.Point(1, 2, 3), Tuple.Vector(0, 1, 0));

            var r2 = r.Transform(Matrix.Identity().Scaling(2, 3, 4).Apply());

            Assert.Equal(r2.Origin, Tuple.Point(2, 6, 12));
            Assert.Equal(r2.Direction, Tuple.Vector(0, 3, 0));
        }
    }
}
