using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Patterns
{
    public class GradientPatternTests
    {
        [Fact]
        public void GradientLinearlyInterpolatesBtwColors()
        {
            var gradient = new GradientPattern(Color._White, Color._Black);
            gradient.PatternAt(Tuple.Point(0, 0, 0)).Should().Be(Color._White);
            gradient.PatternAt(Tuple.Point(0.25, 0, 0)).Should().Be(new Color(.75, .75, .75));
            gradient.PatternAt(Tuple.Point(0.5, 0, 0)).Should().Be(new Color(.5, .5, .5));
            gradient.PatternAt(Tuple.Point(0.75, 0, 0)).Should().Be(new Color(.25, .25, .25));

        }
    }
}
