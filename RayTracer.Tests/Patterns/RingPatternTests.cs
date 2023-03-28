using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Patterns
{
    public class RingPatternTests
    {
        [Fact]
        public void RingShouldExtendInBothXandZ()
        {
            var ring = new RingPattern(Color._White, Color._Black);
            ring.PatternAt(Tuple.Point(0, 0, 0)).Should().Be(Color._White);
            ring.PatternAt(Tuple.Point(1, 0, 0)).Should().Be(Color._Black);
            ring.PatternAt(Tuple.Point(0, 0, 1)).Should().Be(Color._Black);
            ring.PatternAt(Tuple.Point(0.708, 0, 0.708)).Should().Be(Color._Black);

        }
    }
}
