using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests
{
    public class CameraTests
    {
        [Fact]
        public void PixelSizeOfHorizontalCamera()
        {
            Camera c = new Camera(200, 125, Math.PI / 2);
            c.PixelSize.DoubleEqual(0.01).Should().BeTrue();
        }

        [Fact]
        public void PixelSizeOfVerticalCamera()
        {
            Camera c = new Camera(125, 200, Math.PI / 2);
            c.PixelSize.DoubleEqual(0.01).Should().BeTrue();
        }

        [Fact]
        public void RayThroughtCenterOfCanvas()
        {
            Camera c = new Camera(201, 101, Math.PI / 2);
            Ray r = c.RayForPixel(100, 50);
            r.Origin.Should().Be(Tuple.Point(0, 0, 0));
            r.Direction.Should().Be(Tuple.Vector(0, 0, -1));
        }

        [Fact]
        public void RayThroughtCornerOfCanvas()
        {
            Camera c = new Camera(201, 101, Math.PI / 2);
            Ray r = c.RayForPixel(0, 0);
            r.Origin.Should().Be(Tuple.Point(0, 0, 0));
            r.Direction.Should().Be(Tuple.Vector(0.66519, 0.33259, -0.66851));
        }

        [Fact]
        public void RayWhenCameraIsTransformed()
        {
            Camera c = new Camera(201, 101, Math.PI / 2);
            c.Transform = Matrix.Identity().Translation(0, -2, 5).RotateY(Math.PI / 4).Apply();
            Ray r = c.RayForPixel(100, 50);
            r.Origin.Should().Be(Tuple.Point(0, 2, -5));
            r.Direction.Should().Be(Tuple.Vector(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2));
        }
    }
}
