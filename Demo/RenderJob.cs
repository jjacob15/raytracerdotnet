using RayTracer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class RenderJob
    {
        private IWorld World { get; }
        private Canvas Canvas { get; }
        public RendererStats Stats { get; }
        private Camera Camera { get; }

        public List<int> XList = new List<int>();
        public List<int> YList = new List<int>();

        public RenderJob(IWorld world, Camera camera, Canvas canvas, RendererStats stats)
        {
            Camera = camera;
            World = world;
            Canvas = canvas;
            Stats = stats;
        }

        public void Work()
        {
            for (int i = 0; i < XList.Count; i++)
            {
                int x = XList[i];
                int y = YList[i];
                var ray = Camera.RayForPixel(x, y);
                var color = World.ColorAt(ray);
                Canvas[x, y] = color;
                Stats.IncrementPixelCount();
            }
        }
    }
}
