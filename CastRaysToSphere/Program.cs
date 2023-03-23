using RayTracer;
using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using Tuple = RayTracer.Tuple;

namespace CastRaysToSphere
{
    class Program
    {
        static void Main(string[] args)
        {

            //DrawSilhouette();
            Draw3DRender();
            Console.Write("Completed drawing");
        }

        private static void Draw3DRender()
        {
            var rayOrigin = Tuple.Point(0, 0, -5);
            double wallZ = 10;
            double wallSize = 7;

            int size = 200;

            var pixelSize = wallSize / size;

            var half = wallSize / 2;

            var canvas = new Canvas(size, size);
            var sphere = new Sphere
            {
                Material = new Material(new Color(0.5, 0, 0), 0.2, 0.8, 0.8, 300)
            };

            var lightPosition = Tuple.Point(-10, 10, -15);
            var lightColor = new Color(1, 1, 1);
            var light = new PointLight(lightPosition, lightColor);

            //sphere.SetTransform(Matrix.Identity().Scaling(0.5, 1, 1).Shearing(1, 0, 0, 0, 0, 0).Apply());
            for (int y = 0; y <= size; y++)
            {
                double worldY = half - pixelSize * y;

                for (int x = 0; x <= size; x++)
                {
                    double worldX = -half + pixelSize * x;

                    var position = Tuple.Point(worldX, worldY, wallZ);

                    var ray = new Ray(rayOrigin, (position - rayOrigin).Normalize());

                    var xs = new Intersections();
                    sphere.Intersect(ray, xs);
                    if (xs.Hit() != null)
                    {
                        var point = ray.Position(xs.Hit().T);
                        var normal = sphere.NormalAt(point);
                        var eye = -ray.Direction;
                        canvas.SetPixel(x, y, sphere.Material.Lighting(sphere, light, ref point, ref eye, ref normal, false));
                    }
                }
            }
            canvas.Save(@"c:\users\jaison.jacob\desktop\3dSphere.ppm");
        }

        private static void DrawSilhouette()
        {
            var rayOrigin = Tuple.Point(0, 0, -5);
            double wallZ = 10;
            double wallSize = 7;

            int size = 200;

            var pixelSize = wallSize / size;

            var half = wallSize / 2;

            var canvas = new Canvas(size, size);
            var color = new Color(0, 1, 0);
            var shape = new Sphere();

            //shape.SetTransform(Matrix.Identity().Scaling(0.5, 1, 1).Shearing(1, 0, 0, 0, 0, 0).Apply());
            //for each row of pixels in the canvas
            for (int y = 0; y <= size; y++)
            {
                //compute the world y coordinate (top = +half, bottom = -half)
                double worldY = half - pixelSize * y;

                //for each pixel in the row
                for (int x = 0; x <= size; x++)
                {
                    //compute the world x coordinate (left = -half, right = half)
                    double worldX = -half + pixelSize * x;

                    //describe the point on the wall that the ray will target
                    var position = Tuple.Point(worldX, worldY, wallZ);
                    //Console.WriteLine($"x {x} y {y} {position}");
                    var ray = new Ray(rayOrigin, (position - rayOrigin));
                    var xs = new Intersections();
                    shape.Intersect(ray, xs);
                    if (xs.Hit() != null)
                        canvas.SetPixel(x, y, color);
                }
            }
            canvas.Save(@"c:\users\jaison.jacob\desktop\sphere.ppm");
        }
    }
}
