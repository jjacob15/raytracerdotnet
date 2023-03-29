﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class RendererParameters
    {
        public static RendererParameters LowQuality => new RendererParameters { Width = 200, Height = 50 };
        public static RendererParameters DefaultQuality => new RendererParameters { Width = 400, Height = 200 };
        public static RendererParameters HighQuality => new RendererParameters { Width = 640, Height = 400 };

        public int Height { get; set; }
        public int Width { get; set; }

    }
}