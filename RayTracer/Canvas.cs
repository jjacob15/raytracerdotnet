using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Canvas
    {
        private Color[][] canvas;
        private CanvasWriter writer;
        private object _lock = new object();
        public Canvas(int w, int h)
        {
            Width = w;
            Height = h;
            canvas = new Color[w][];
            writer = new CanvasWriter(this);

            for (int i = 0; i < Width; i++)
            {
                canvas[i] = new Color[h];
                //for (int j = 0; j < h; j++)
                //    canvas[i][j] = background;
            }
        }

        //private void Initialize(Color background)
        //{
        //    for (int i = 0; i < Width; i++)
        //        for (int j = 0; j < Height; j++)
        //            canvas[i, j] = background;
        //}

        public double Width { get; }
        public double Height { get; }

        public Color this[int w, int h]
        {
            get => canvas[w][h];
            set
            {
                canvas[w][h] = value;
            }
        }

        public void SetPixel(int x, int y, Color c)
        {
            lock (_lock)
            {
                canvas[x][y] = c;
            }
        }

        public string GetContent()
        {
            return writer.CreateContent();
        }
        public void Save(string filename = "file.ppm")
        {
            writer.Save(filename);
        }
    }
}
