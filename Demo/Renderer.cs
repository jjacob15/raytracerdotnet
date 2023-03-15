using RayTracer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tuple = RayTracer.Tuple;

namespace Demo
{
    public class Renderer
    {
        private string Output { get; }
        private Canvas Canvas { get; set; }
        private ConcurrentQueue<RenderJob> RenderJobs { get; } = new ConcurrentQueue<RenderJob>();

        public RendererStats Stats { get; private set; }

        public Renderer() : this(Path.GetTempPath())
        {
        }

        public Renderer(string outputDir)
        {
            Output = outputDir;
        }




        public void Render(Type type, RendererParameters renderParams, int threads)
        {
            Scene scene = (Scene)Activator.CreateInstance(type);
            if (scene == null)
                throw new Exception($"Invalid scene type: {type.FullName}");

            scene.Initialize();
            Render(scene, renderParams, threads);
        }

        private void Render(Scene scene, RendererParameters renderParameters, int threads)
        {
            Canvas = new Canvas(renderParameters.Width, renderParameters.Height);
            var camera = BuildCamera(renderParameters);

            Stats = new RendererStats(renderParameters.Width * renderParameters.Height);
            Render(scene.World, Canvas, camera, threads);
        }

        private void Render(IWorld world, Canvas canvas, Camera camera, int threadCount)
        {
            var point = new List<Tuple<int, int>>();
            for (int y = 0; y < camera.VSize; y++)
            {
                for (int x = 0; x < camera.HSize; x++)
                {
                    point.Add(new Tuple<int, int>(x, y));
                }
            }

            const int Batch = 1000;
            for (int i = 0; i < point.Count; i += Batch)
            {
                RenderJob job = new RenderJob(world, camera, canvas, Stats);
                for (int j = 0; j < Batch; j++)
                {
                    if (i + j >= point.Count)
                    {
                        continue;
                    }
                    var pixel = point[i + j];
                    job.XList.Add(pixel.Item1);
                    job.YList.Add(pixel.Item2);
                }
                RenderJobs.Enqueue(job);
            }

            Thread[] threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(RunTask) { Name = $"worker_{i}" };
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Stats.Stop();
        }

        public string Save(string name)
        {
            var path = Path.Combine(Output, name);
            Canvas.Save(path);
            return path;
        }

        private void RunTask()
        {
            Console.WriteLine($"In worker {Thread.CurrentThread.Name}");
            while (RenderJobs.TryDequeue(out var renderJob))
            {
                renderJob.Work();
            }
        }

        private Camera BuildCamera(RendererParameters rendererParameters)
        {
            var c = new Camera(rendererParameters.Width, rendererParameters.Height, Math.PI / 3);
            c.SetTransform(Matrix.ViewTransform(Tuple.Point(0, 1.5, -5),
              Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0)));
            return c;
        }
    }
}
