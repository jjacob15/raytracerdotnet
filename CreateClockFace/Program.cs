using RayTracer;
using System;
using Tuple = RayTracer.Tuple;

namespace CreateClockFace
{
    class Program
    {
        static void Main(string[] args)
        {
            //DrawClock();
            DrawLine();
            Console.Read();
        }

        private static void DrawLine()
        {
            Color color = new Color(1, 1, 1);
            int n = 12;
            var size = 200;
            Canvas c = new Canvas(size, size);
            var point = Tuple.Point(size / 2, 0, 0);
            Console.WriteLine(point);
            point = Matrix.Transformation().Scaling(.5, 0, .5).Apply() * point;
            Console.WriteLine(point);
            var translation = Matrix.Transformation().RotateY(Math.PI / 6).Apply();
            for (int i = 0; i < n; i++)
            {
                var pX = (int)(point.X + (size/2));
                var pY = (int)(point.Z + (size / 2));
                Console.WriteLine(point);
                if (pX > 0 && pY > 0)
                    c[pX, pY] = color;
                point = translation * point;
            }

            c.Save(@"c:\users\jaison.jacob\desktop\line.ppm");
            Console.WriteLine("Drew line");
        }

        private static void DrawClock()
        {
            Color color = new Color(1, 1, 1);
            int n = 12;
            var size = 200;
            Canvas c = new Canvas(size, size);
            var point = Tuple.Point(0, 0, 0);
            var transform = Matrix.Transformation().Translation(0, 1, 0).Scaling(size / 4, size / 4, 0).Apply();//.Scaling(size / 4, size / 4, 0).Apply();
            point = transform * point;

            var rotation = Matrix.Transformation().RotateZ(Math.PI / 6).Apply();

            for (int i = 0; i < n; i++)
            {
                point = rotation * point;
                Console.WriteLine($"x {(int)point.X + size / 2} y {(int)point.Y + size / 2} point x {(int)point.X} point y {(int)point.Y}");
                var pX = (int)(point.X + size / 2);
                var pY = (int)(point.Y + size / 2);
                c[pX, pY] = color;
            }


            c.Save(@"c:\users\jaison.jacob\desktop\clock.ppm");
            Console.WriteLine("Drew clock");
        }
    }
}
