using RayTracer;
using RayTracer.Lights;
using System;
using Tuple = RayTracer.Tuple;

namespace CreateAScene
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = CreateAScene();
            c.Save(@"c:\\users\\jaison.jacob\\desktop\\simplescene.ppm");
            Console.WriteLine("Hello World!");
        }

        static Canvas CreateAScene()
        {
            World w = new World();

            var floor = new Sphere();
            floor.Transform = Matrix.Identity().Scaling(10, 0.01, 10).Apply();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;
            w.AddShape(floor);

            var leftWall = new Sphere();
            leftWall.Transform = Matrix.Identity().Scaling(10, 0.01, 10).RotateX(Math.PI / 2)
                .RotateY(-Math.PI / 4).Translation(0, 0, 5).Apply();
            leftWall.Material = floor.Material;
            w.AddShape(leftWall);

            var rightWall = new Sphere();
            rightWall.Transform = Matrix.Identity().Scaling(10, 0.01, 10).RotateX(Math.PI / 2)
                 .RotateY(Math.PI / 4).Translation(0, 0, 5).Apply();
            rightWall.Material = floor.Material;
            w.AddShape(rightWall);

            var middle = new Sphere();
            middle.Transform = Matrix.Identity().Translation(-0.5, 1, 0.5).Apply();
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            w.AddShape(middle);

            var right = new Sphere();
            right.Transform = Matrix.Identity().Scaling(0.5, 0.5, 0.5).Translation(1.5, 0.5, -0.5).Apply();
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            w.AddShape(right);


            var left = new Sphere();
            left.Transform = Matrix.Identity().Scaling(.33, .33, .33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            w.AddShape(left);

            w.Light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1));

            Camera c = new Camera(800, 400, Math.PI / 3);
            c.Transform = Matrix.ViewTransform(Tuple.Point(0, 1.5, -5), 
                Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0));
            return c.Render(w);
        }
    }
}
