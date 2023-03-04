using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RayTracer
{
    public class CanvasWriter
    {
        private string _desktop = @"c:\users\jaison.jacob\desktop";
        private Canvas canvas;
        public CanvasWriter(Canvas canvas)
        {
            this.canvas = canvas;
        }
        private string CreateHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine($"{canvas.Width} {canvas.Height}");
            sb.AppendLine($"255");
            return sb.ToString();
        }

        public void Save(string filename)
        {
            var body = CreateContent();
            using FileStream fs = File.Create(filename);
            fs.Write(Encoding.UTF8.GetBytes(body));
        }

        public string CreateContent()
        {
            var header = CreateHeader();
            StringBuilder sb = new StringBuilder();
            sb.Append(header);
            for (int h = 0; h < canvas.Height; h++)
            {
                int lineLength = 0;
                for (int w = 0; w < canvas.Width; w++)
                {
                    string[] colors = canvas[w, h].ToStringArr();
                    foreach (string color in colors)
                    {
                        lineLength += color.Length + 1;
                        if (lineLength > 70)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.AppendLine();
                            lineLength = 0;
                        }
                        sb.Append(color);
                        sb.Append(" ");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
