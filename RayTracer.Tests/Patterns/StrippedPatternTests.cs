using FluentAssertions;
using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Patterns
{
    public class StrippedPatternTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ConstantY(int y)
        {
            var pattern = new StrippedPattern(Color._White, Color._Black);
            pattern.PatternAt(Tuple.Point(0, y, 0)).Should().Be(Color._White);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ConstantZ(int z)
        {
            var pattern = new StrippedPattern(Color._White, Color._Black);
            pattern.PatternAt(Tuple.Point(0, 0, z)).Should().Be(Color._White);
        }


        [Fact]
        public void AlternatingX()
        {
            var pattern = new StrippedPattern(Color._White, Color._Black);
            pattern.PatternAt(Tuple.Point(0, 0, 0)).Should().Be(Color._White);
            pattern.PatternAt(Tuple.Point(0.9, 0, 0)).Should().Be(Color._White);
            pattern.PatternAt(Tuple.Point(1, 0, 0)).Should().Be(Color._Black);
            pattern.PatternAt(Tuple.Point(-0.1, 0, 0)).Should().Be(Color._Black);
            pattern.PatternAt(Tuple.Point(-1, 0, 0)).Should().Be(Color._Black);
        }

        [Fact]
        public void LightingWithAPattern()
        {
            Material m = new Material();
            m.Pattern = new StrippedPattern(Color._White, Color._Black);
            m.Ambient = 1;
            m.Diffuse = 0;
            m.Specular = 0;
            var eyeV = Tuple.Vector(0, 0, -1);
            var normalV = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            var point1 = Tuple.Point(0.9, 0, 0);
            var point2 = Tuple.Point(1.1, 0, 0);
            m.Lighting(new Sphere(), light, ref point1, ref eyeV, ref normalV, false).Should().Be(Color._White);
            m.Lighting(new Sphere(), light, ref point2, ref eyeV, ref normalV, false).Should().Be(Color._Black);
        }

        [Fact]
        public void StripesWithAnObjectTransformation()
        {
            Sphere s = new Sphere();
            s.Transform = Matrix.Transformation().Scaling(2, 2, 2).Apply();
            var pattern = new StrippedPattern();
            var point = Tuple.Point(1.5, 0, 0);
            pattern.PatternAtShape(s, ref point).Should().Be(Color._White);
        }

        [Fact]
        public void StripesWithAnPatternTransformation()
        {
            Sphere s = new Sphere();
            var pattern = new StrippedPattern();
            pattern.Transform = Matrix.Transformation().Scaling(2, 2, 2).Apply();
            var point = Tuple.Point(1.5, 0, 0);
            pattern.PatternAtShape(s, ref point).Should().Be(Color._White);
        }

        [Fact]
        public void StripesWithBothObjectAndPatternTransformation()
        {
            Sphere s = new Sphere();
            s.Transform = Matrix.Transformation().Scaling(2, 2, 2).Apply();
            var pattern = new StrippedPattern();
            pattern.Transform = Matrix.Transformation().Translation(0.5, 0, 0).Apply();
            var point = Tuple.Point(2.5, 0, 0);
            pattern.PatternAtShape(s,ref point).Should().Be(Color._White);
        }
    }
}
