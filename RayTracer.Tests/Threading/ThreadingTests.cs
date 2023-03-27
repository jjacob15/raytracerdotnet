using FluentAssertions;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;

namespace RayTracer.Threading
{

    public class ReflectionScene : AbstractScene
    {
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(new StrippedPattern(Color.White, Color.Black), specular: 0, reflective: 0);
            floor.Transform = Matrix.Transformation().Translation(0, 0, 0).Apply();

            var middle = new Sphere();
            middle.Transform = Matrix.Transformation().Translation(0, 1, 0.5).Apply();
            middle.Material = new Material(new Color(0.8, 1, 0.1), diffuse: 0.7, specular: 0.3, reflective: 1);

            var right = new Sphere();
            right.Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Translation(1.5, 0.5, -0.5).Apply();
            right.Material = new Material(new GradientPattern(new Color(0, 0, 1), new Color(1, 0.5, 0.1))
            { Transform = Matrix.Transformation().Scaling(0.25, 1, 1).RotateY(180).Apply() },
            diffuse: 0.7, specular: 1, reflective: 1);

            var left = new Sphere();
            left.Transform = Matrix.Transformation().Scaling(0.33, 0.33, 0.33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material = new Material(new CheckerPattern(new Color(1, 0, 0), new Color(0, 1, 0))
            { Transform = Matrix.Transformation().Scaling(0.25, 0.25, 0.25).Apply() }, diffuse: 0.7,
            specular: 0.3,
            reflective: 1);

            Add(new[] {
                floor,middle, left, right });
        }
    }
    public class ThreadingTests
    {
        [Fact]
        public void ColorAtWithoutThreads()
        {
            AbstractScene scene = (AbstractScene)Activator.CreateInstance(typeof(ReflectionScene));
            World w = (World)scene.World;

            scene.Initialize();

            ConcurrentQueue<(int x, int y, double r, double g, double b)> jobs = new ConcurrentQueue<(int x, int y, double r, double g, double b)>();
            int errorCount = 0;
            ConcurrentBag<Tuple<int, int, Color, Color>> result = new ConcurrentBag<Tuple<int, int, Color, Color>>();
            using StreamReader sr = new StreamReader(@".\Threading\test.txt");
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var parts = line.Split(",");

                    var x = Convert.ToInt32(parts[0]);
                    var y = Convert.ToInt32(parts[1]);
                    var r = Convert.ToDouble(parts[2]);
                    var g = Convert.ToDouble(parts[3]);
                    var b = Convert.ToDouble(parts[4]);

                    jobs.Enqueue((x, y, r, g, b));
                }
            }

            var c = new CameraSettings().Build();

            while (jobs.TryDequeue(out var job))
            {
                var ray = c.RayForPixel(job.x, job.y);
                var outcolor = w.ColorAt(ray);
                if (!outcolor.Red.DoubleEqual(job.r) || !outcolor.Green.DoubleEqual(job.g) ||
                    !outcolor.Blue.DoubleEqual(job.b))
                {
                    Interlocked.Increment(ref errorCount);
                    result.Add(new Tuple<int, int, Color, Color>(job.x, job.y, new Color(job.r, job.g, job.b), new Color(outcolor.Red, outcolor.Green, outcolor.Blue)));
                }
            }

            Assert.Equal(0, errorCount);
        }

        [Fact(Skip = "")]
        public void ColorAtWithThreading()
        {
            AbstractScene scene = (AbstractScene)Activator.CreateInstance(typeof(ReflectionScene));
            World w = (World)scene.World;

            scene.Initialize();

            ConcurrentQueue<(int x, int y, double r, double g, double b)> jobs = new ConcurrentQueue<(int x, int y, double r, double g, double b)>();
            int errorCount = 0;
            ConcurrentBag<Tuple<int, int, Color, Color>> result = new ConcurrentBag<Tuple<int, int, Color, Color>>();
            using StreamReader sr = new StreamReader(@".\Threading\test.txt");
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var parts = line.Split(",");

                    var x = Convert.ToInt32(parts[0]);
                    var y = Convert.ToInt32(parts[1]);
                    var r = Convert.ToDouble(parts[2]);
                    var g = Convert.ToDouble(parts[3]);
                    var b = Convert.ToDouble(parts[4]);

                    jobs.Enqueue((x, y, r, g, b));
                }
            }

            var c = new CameraSettings().Build();

            void localTask()
            {
                while (jobs.TryDequeue(out var job))
                {
                    var ray = c.RayForPixel(job.x, job.y);
                    var outcolor = w.ColorAt(ray);
                    if (!outcolor.Red.DoubleEqual(job.r) || !outcolor.Green.DoubleEqual(job.g) ||
                        !outcolor.Blue.DoubleEqual(job.b))
                    {
                        Interlocked.Increment(ref errorCount);
                        result.Add(new Tuple<int, int, Color, Color>(job.x, job.y, new Color(job.r, job.g, job.b), new Color(outcolor.Red, outcolor.Green, outcolor.Blue)));
                    }
                }
            }

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
            {
                var t = new Thread(localTask) { Name = $"Job thread {i} " };
                threads.Add(t);
                t.Start();

            }

            foreach (var t in threads)
                t.Join();

            Assert.Equal(0, errorCount);
        }
    }
}
