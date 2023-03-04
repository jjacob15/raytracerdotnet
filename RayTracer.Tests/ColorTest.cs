using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class ColorTest
    {
        [Fact]
        public void AddingColor()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);

            Assert.Equal(c1 + c2, new Color(1.6, 0.7, 1.0));
        }

        [Fact]
        public void SubtractingColor()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);

            Assert.Equal(c1 - c2, new Color(0.2, 0.5, 0.5));
        }

        [Fact]
        public void MultiplyingColor()
        {
            var c1 = new Color(1, 0.2, 0.4);
            var c2 = new Color(0.9, 1, 0.1);

            Assert.Equal(c1 * c2, new Color(0.9, 0.2, 0.04));
        }

        [Fact]
        public void MultiplyingScalarColor()
        {
            var c1 = new Color(0.2, 0.3, 0.4);

            Assert.Equal(c1 * 2, new Color(0.4, 0.6, 0.8));
        }
    }
}
