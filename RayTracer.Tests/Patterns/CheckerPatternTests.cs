using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Patterns
{
    public class CheckerPatternTests
    {
        [Fact]
        public void ShouldRepeatInX()
        {
            var checker = new CheckerPattern(Color.White, Color.Black);
            checker.PatternAt(Tuple.Point(0, 0, 0)).Should().Be(Color.White);
            checker.PatternAt(Tuple.Point(0.99, 0, 0)).Should().Be(Color.White);
            checker.PatternAt(Tuple.Point(1.01, 0, 0)).Should().Be(Color.Black);
        }

        [Fact]
        public void ShouldRepeatInY()
        {
            var checker = new CheckerPattern(Color.White, Color.Black);
            checker.PatternAt(Tuple.Point(0, 0, 0)).Should().Be(Color.White);
            checker.PatternAt(Tuple.Point(0, 0.99, 0)).Should().Be(Color.White);
            checker.PatternAt(Tuple.Point(0, 1.01, 0)).Should().Be(Color.Black);
        }

        [Fact]
        public void ShouldRepeatInZ()
        {
            var checker = new CheckerPattern(Color.White, Color.Black);
            checker.PatternAt(Tuple.Point(0, 0, 0)).Should().Be(Color.White);
            checker.PatternAt(Tuple.Point(0, 0, 0.99)).Should().Be(Color.White);
            checker.PatternAt(Tuple.Point(0, 0, 1.01)).Should().Be(Color.Black);
        }
    }
}
