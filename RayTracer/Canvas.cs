using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Canvas
    {
        private Color[][] canvas;
        private CanvasWriter writer;

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
            set => canvas[w][h] = value;
        }

        public string GetContent()
        {
            return writer.CreateContent();
        }
        public void Save(string filename="file.ppm")
        {
            writer.Save(filename);
        }
    }
}
