using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class TranslationTests
    {
        [Fact]
        public void TransformMultiplication()
        {
            Matrix tranform = Matrix.Identity().Translation(5, -3, 2).Apply();
            Tuple point = Tuple.Point(-3, 4, 5);

            Assert.True(tranform * point == Tuple.Point(2, 1, 7));
        }

        [Fact]
        public void InverseTransformMultiplication()
        {
            Matrix tranform = Matrix.Identity().Translation(5, -3, 2).Apply();
            var inverse = tranform.Inverse();
            Tuple point = Tuple.Point(-3, 4, 5);

            Assert.True(inverse * point == Tuple.Point(-8, 7, 3));
        }

        [Fact]
        public void TransformVectorMultiplication()
        {
            Matrix tranform = Matrix.Identity().Translation(5, -3, 2).Apply();
            Tuple vector = Tuple.Vector(-3, 4, 5);

            Assert.True(tranform * vector == vector);
        }

        [Fact]
        public void ScalingPoint()
        {
            Matrix tranform = Matrix.Identity().Scaling(2, 3, 4).Apply();
            Tuple point = Tuple.Point(-4, 6, 8);

            Assert.True(tranform * point == Tuple.Point(-8, 18, 32));
        }

        [Fact]
        public void ScalingVector()
        {
            Matrix tranform = Matrix.Identity().Scaling(2, 3, 4).Apply();
            Tuple point = Tuple.Vector(-4, 6, 8);

            Assert.True(tranform * point == Tuple.Vector(-8, 18, 32));
        }

        [Fact]
        public void MultiplyInverseOfAScale()
        {
            Matrix tranform = Matrix.Identity().Scaling(2, 3, 4).Apply();
            var inv = tranform.Inverse();
            Tuple vector = Tuple.Vector(-4, 6, 8);

            Assert.True(inv * vector == Tuple.Vector(-2, 2, 2));
        }

        [Fact]
        public void Reflection()
        {
            Matrix tranform = Matrix.Identity().Scaling(-1, 1, 1).Apply();
            Tuple point = Tuple.Point(2,3,4);

            Assert.True(tranform * point == Tuple.Point(-2, 3, 4));
        }
      


    }
}
