using Demo;
using Demo.Scenes;
using RayTracer;
using RayTracer.Lights;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.IO;
using Tuple = RayTracer.Tuple;

namespace CreateAScene
{
    class Program
    {
        static void Main(string[] args)
        {
            //var c = CreateAScene();
            //var c = CustomScene();
            //var c = CanvasWithPlane();
            //var c = SimpleSceneWithReflection();
            //c.Save(@"c:\\users\\jaison.jacob\\desktop\\simplescene1.ppm");
            WriteEachPoint();
            Console.WriteLine("Hello World!");
        }

        static void WriteEachPoint()
        {

            AbstractScene scene = (AbstractScene)Activator.CreateInstance(typeof(ReflectionScene));

            scene.Initialize();


            using StreamWriter sw = new StreamWriter(@"d:\test\picture1.txt");
            var c = Camera.DefaultCamera();
            for (int x = 0; x < 640; x++)
            {
                for (int y = 0; y < 400; y++)
                {
                    var ray = c.RayForPixel(x, y);
                    var color = scene.World.ColorAt(ray, 10);
                    sw.WriteLine($"{x},{y},{color.Red},{color.Green},{color.Blue}");
                }
            }

        }
        static Canvas SimpleSceneWithReflection()
        {
            World w = new World();


            var floor = new Plane();
            floor.Material.Pattern = new CheckerPattern(new Color(1, 0.9, 0.9), new Color(0.6, 0, 0));
            floor.Material.Reflective = 0.5;
            //floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;
            w.AddShape(floor);

            var leftWall = new Plane();
            leftWall.Transform = Matrix.Transformation().RotateX(Math.PI / 2)
                .RotateY(-Math.PI / 4).Translation(0, 0, 5).Apply();
            leftWall.Material = new Material(new Color(1, 0.9, 0.9));
            leftWall.Material.Reflective = 1;
            leftWall.Material.Transparency = 0.5;
            w.AddShape(leftWall);

            var rightWall = new Plane();
            rightWall.Transform = Matrix.Transformation().RotateX(Math.PI / 2)
                 .RotateY(Math.PI / 4).Translation(0, 0, 5).Apply();
            rightWall.Material = new Material(new Color(1, 1, 1));
            rightWall.Material.Reflective = 1;
            rightWall.Material.Transparency = 0.5;
            w.AddShape(rightWall);

            var right = new Sphere();
            right.Transform = Matrix.Transformation().Translation(0.5, 1, 1).Apply();
            right.Material = new Material(new Color(0.5, 1, 0.1));
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            w.AddShape(right);


            var left = Sphere.GlassSphere();
            left.Transform = Matrix.Transformation().Scaling(.33, .33, .33).Translation(-1.5, 0.33, -0.75).Apply();
            //left.Material.Specular = 0.3;
            w.AddShape(left);

            w.SetLight(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));

            Camera c = new Camera(400, 200, Math.PI / 3, Matrix.ViewTransform(Tuple.Point(0, 1.5, -5),
                Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0)));
            return c.Render(w);
        }

        static Canvas CanvasWithPlane()
        {
            World w = new World();


            var floor = new Plane();
            floor.Material.Pattern = new RayTracer.Patterns.CheckerPattern(new Color(1, 0.9, 0.9), new Color(0.6, 0, 0));
            //floor.Transform = Matrix.Transformation().Apply();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;
            w.AddShape(floor);

            var wall = new Plane();
            wall.Material.Pattern = new RayTracer.Patterns.StrippedPattern(new Color(1, 0.9, 0.9), new Color(0.6, 0, 0));
            wall.Transform = Matrix.Transformation().RotateX(Math.PI / 2).Translation(0, 0, 5).Apply();
            wall.Material.Color = new Color(1, 0.9, 0.9);
            wall.Material.Specular = 0;
            w.AddShape(wall);

            var right = new Sphere();
            right.Material.Pattern = new RayTracer.Patterns.StrippedPattern(new Color(1, 0, 0), new Color(0, 1, 0));
            right.Transform = Matrix.Transformation().Translation(0.5, 1, 1).Apply();
            //right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            w.AddShape(right);


            var left = new Sphere();
            left.Transform = Matrix.Transformation().Scaling(.33, .33, .33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            w.AddShape(left);

            w.SetLight(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));

            Camera c = new Camera(100, 50, Math.PI / 3, Matrix.ViewTransform(Tuple.Point(0, 1.5, -5),
                Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0)));
            return c.Render(w);
        }
        static Canvas CustomScene()
        {
            World w = new World();
            var screen = new Sphere();
            screen.Transform = Matrix.Transformation().Scaling(20, 0.1, 20).RotateX(Math.PI / 2).Translation(0, 0, 10).Apply();
            screen.Material.Color = new Color(0.5, 0.5, .5);
            screen.Material.Specular = 0;
            w.AddShape(screen);

            var sphere = new Sphere();
            sphere.Transform = Matrix.Transformation().Translation(1.5, 1, 1.5).Apply();
            sphere.Material.Color = new Color(0.1, 1, 0.5);
            sphere.Material.Diffuse = 0.7;
            sphere.Material.Specular = 0.3;
            w.AddShape(sphere);


            var sphere2 = new Sphere();
            sphere2.Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Translation(-0.5, 1, -0.5).Apply();
            sphere2.Material.Color = new Color(1, 1, 0);
            sphere2.Material.Diffuse = 0.7;
            sphere2.Material.Specular = 0.3;
            w.AddShape(sphere2);


            w.SetLight(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));

            Camera c = new Camera(100, 50, Math.PI / 3, Matrix.ViewTransform(Tuple.Point(0, 1.5, -7),
                Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0)));
            return c.Render(w);
        }

        static Canvas CreateAScene()
        {
            World w = new World();

            var floor = new Sphere();
            floor.Transform = Matrix.Transformation().Scaling(10, 0.01, 10).Apply();
            floor.Material.Color = new Color(1, 0.9, 0.9);
            floor.Material.Specular = 0;
            w.AddShape(floor);

            var leftWall = new Sphere();
            leftWall.Transform = Matrix.Transformation().Scaling(10, 0.01, 10).RotateX(Math.PI / 2)
                .RotateY(-Math.PI / 4).Translation(0, 0, 5).Apply();
            leftWall.Material = floor.Material;
            w.AddShape(leftWall);

            var rightWall = new Sphere();
            rightWall.Transform = Matrix.Transformation().Scaling(10, 0.01, 10).RotateX(Math.PI / 2)
                 .RotateY(Math.PI / 4).Translation(0, 0, 5).Apply();
            rightWall.Material = floor.Material;
            w.AddShape(rightWall);

            var middle = new Sphere();
            middle.Transform = Matrix.Transformation().Translation(-0.5, 1, 0.5).Apply();
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            w.AddShape(middle);

            var right = new Sphere();
            right.Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Translation(1.5, 0.5, -0.5).Apply();
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            w.AddShape(right);


            var left = new Sphere();
            left.Transform = Matrix.Transformation().Scaling(.33, .33, .33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            w.AddShape(left);

            w.SetLight(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));

            Camera c = new Camera(800, 400, Math.PI / 3, Matrix.ViewTransform(Tuple.Point(0, 1.5, -5),
                Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0)));
            return c.Render(w);
        }
    }
}
