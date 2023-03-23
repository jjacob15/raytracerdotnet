using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Canvas
    {
        private Color[][] canvas;
        private bool[][] completed;

        private CanvasWriter writer;
        public Canvas(int w, int h)
        {
            Width = w;
            Height = h;
            canvas = new Color[w][];
            completed = new bool[w][];
            writer = new CanvasWriter(this);

            for (int i = 0; i < Width; i++)
            {
                canvas[i] = new Color[h];
                completed[i] = new bool[h];
            }
        }

        public double Width { get; }
        public double Height { get; }

        public Color this[int w, int h]
        {
            get => canvas[w][h];
        }

        public void SetPixel(int x, int y, Color c)
        {
            canvas[x][y] = c;
            completed[x][y] = true;
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
