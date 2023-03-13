using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer
{
    public class TransformationTests
    {
        [Fact]
        public void TransformationSequenceTest()
        {
            var p = Tuple.Point(1, 0, 1);
            var a = Matrix.Identity().RotateX(Math.PI / 2).Apply();
            var b = Matrix.Identity().Scaling(5, 5, 5).Apply();
            var c = Matrix.Identity().Translation(10, 5, 7).Apply();

            var p2 = a * p;

            Assert.True(p2 == Tuple.Point(1, -1, 0));

            var p3 = b * p2;

            Assert.True(p3 == Tuple.Point(5, -5, 0));

            var p4 = c * p3;

            Assert.True(p4 == Tuple.Point(15, 0, 7));
        }

        [Fact]
        public void TransformationChainTest()
        {
            var p = Tuple.Point(1, 0, 1);
            var transform = Matrix.Identity()
                .RotateX(Math.PI / 2)
                .Scaling(5, 5, 5)
                .Translation(10, 5, 7).Apply();
            var output = transform * p;
            Assert.True(output == Tuple.Point(15, 0, 7));
        }
    }
}