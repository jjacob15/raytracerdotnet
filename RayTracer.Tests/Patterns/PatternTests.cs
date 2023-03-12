using FluentAssertions;
using RayTracer.Patterns;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.Patterns
{
    public class PatternTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ConstantY(int y)
        {
            var pattern = new StrippedPattern(Color.White, Color.Black);
            pattern.StipeAt(Tuple.Point(0, y, 0)).Should().Be(Color.White);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ConstantZ(int z)
        {
            var pattern = new StrippedPattern(Color.White, Color.Black);
            pattern.StipeAt(Tuple.Point(0, 0, z)).Should().Be(Color.White);
        }


        [Fact]
        public void AlternatingX()
        {
            var pattern = new StrippedPattern(Color.White, Color.Black);
            pattern.StipeAt(Tuple.Point(0, 0, 0)).Should().Be(Color.White);
            pattern.StipeAt(Tuple.Point(0.9, 0, 0)).Should().Be(Color.White);
            pattern.StipeAt(Tuple.Point(1, 0, 0)).Should().Be(Color.Black);
            pattern.StipeAt(Tuple.Point(-0.1, 0, 0)).Should().Be(Color.Black);
            pattern.StipeAt(Tuple.Point(-1, 0, 0)).Should().Be(Color.Black);
        }
    }
}
