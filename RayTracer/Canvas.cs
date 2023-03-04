using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Canvas
    {
        private Color[,] canvas;
        private CanvasWriter writer;

        public Canvas(int w, int h) : this(w, h, Color.Black) { }
        public Canvas(int w, int h, Color background)
        {
            Width = w;
            Height = h;
            canvas = new Color[w, h];
            writer = new CanvasWriter(this);

            Initialize(background);
        }

        private void Initialize(Color background)
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    canvas[i, j] = background;
        }

        public double Width { get; }
        public double Height { get; }

        public Color this[int w, int h]
        {
            get => canvas[w, h];
            set => canvas[w, h] = value;
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
