using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace RayTracer
{
    public class CanvasTest
    {
        [Fact]
        public void Initialization()
        {
            var c = new Canvas(10, 20);

            for (int w = 0; w < c.Width; w++)
                for (int h = 0; h < c.Height; h++)
                    Assert.Equal(c[w, h], Color._Black);
        }

        [Fact]
        public void WriteToPixel()
        {
            var c = new Canvas(10, 20);
            var red = new Color(1, 0, 0);
            c.SetPixel(2, 3, red);
            Assert.Equal(c[2, 3], red);
        }

        [Fact]
        public void ConstructHeader()
        {
            var c = new Canvas(5, 3);
            var ppm = c.GetContent();
            var ppmLines = GetLines(ppm);

            Assert.Equal("P3", ppmLines[0]);
            Assert.Equal("5 3", ppmLines[1]);
            Assert.Equal("255", ppmLines[2]);
        }

        private List<string> GetLines(string ppm)
        {
            List<string> lines = new List<string>();
            using StringReader sr = new StringReader(ppm);
            while (true)
            {
                var line = sr.ReadLine();
                if (line != null)
                    lines.Add(line);
                else
                    break;
            }
            return lines;
        }

        [Fact]
        public void ConstructBody()
        {
            var c = new Canvas(5, 3);
            c.SetPixel(0, 0, new Color(1.5, 0, 0));
            c.SetPixel(2, 1, new Color(0, 0.5, 0));
            c.SetPixel(4, 2, new Color(-0.5, 0, 1));

            var ppm = c.GetContent();
            var ppmLines = GetLines(ppm);

            Assert.Equal("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", ppmLines[3]);
            Assert.Equal("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0", ppmLines[4]);
            Assert.Equal("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", ppmLines[5]);
        }

        [Fact]
        public void NewLineEndingTest()
        {
            var c = new Canvas(5, 2);
            var ppm = c.GetContent();
            Assert.EndsWith(Environment.NewLine,ppm);
        }
    }
}
