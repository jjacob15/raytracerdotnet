using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class RendererParameters
    {
        public static RendererParameters LowQuality => new RendererParameters { Width = 100, Height = 200 };
        public static RendererParameters HighQuality => new RendererParameters { Width = 640, Height = 400 };

        public int Height { get; set; }
        public int Width { get; set; }

    }
}
