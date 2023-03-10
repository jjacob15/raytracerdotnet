using RayTracer;
using System;

namespace CreateAScene
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static Canvas CreatingScene()
        {
            var floor = new Sphere();
            floor.Transform = Matrix.Identity().Scaling(10, 0.01, 10).Apply();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;

            var leftWall = new Sphere();
            leftWall.Transform = Matrix.Identity().Scaling(10, 0.01, 10).RotateX(Math.PI / 2)
                .RotateY(-Math.PI / 4).Translation(0, 0, 5).Apply();
            leftWall.Material = floor.Material;

            var rightWall = new Sphere();
            rightWall.Transform = Matrix.Identity().Scaling(10, 0.01, 10).RotateX(Math.PI / 2)
                 .RotateY(Math.PI / 4).Translation(0, 0, 5).Apply();

            var middle = new Sphere();
            middle.Transform = Matrix.Identity().Translation(-0.5, 1, 0.5).Apply();
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;

            var right = new Sphere();
            right.Transform = Matrix.Identity().Scaling(0.5, 0.5, 0.5).Translation(1.5, 0.5, -0.5).Apply();
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;


            var left = new Sphere();
            left.Transform = Matrix.Identity().Scaling(.33, .33, .33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;

            World w = new World();

            return null;
        }
    }
}
