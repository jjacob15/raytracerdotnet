using RayTracer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private Thread[] threads;
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
            AbstractScene scene = (AbstractScene)Activator.CreateInstance(type);
            if (scene == null)
                throw new Exception($"Invalid scene type: {type.FullName}");

            scene.Initialize();
            Render(scene, renderParams, threads);
        }

        private void Render(AbstractScene scene, RendererParameters renderParameters, int threads)
        {
            Canvas = new Canvas(renderParameters.Width, renderParameters.Height);
            var camera = BuildCamera(renderParameters);

            Stats = new RendererStats(renderParameters.Width * renderParameters.Height);
            Render(scene.World, Canvas, camera, threads);
        }

        private void Render(IWorld world, Canvas canvas, Camera camera, int threadCount)
        {
            var pixels = new List<Tuple<int, int>>();
            for (int y = 0; y < camera.VSize; y++)
            {
                for (int x = 0; x < camera.HSize; x++)
                {
                    pixels.Add(new Tuple<int, int>(x, y));
                }
            }

            const int BatchSize = 128;
            for (int i = 0; i < pixels.Count; i += BatchSize)
            {
                RenderJob job = new RenderJob(world, camera,canvas, Stats);
                for (int j = 0; j < BatchSize; j++)
                {
                    if (i + j >= pixels.Count)
                    {
                        continue;
                    }
                    var pixel = pixels[i + j];
                    job.XList.Add(pixel.Item1);
                    job.YList.Add(pixel.Item2);
                }
                RenderJobs.Enqueue(job);
            }
            //int batchSize = 500;
            //int count = 0;
            //RenderJob job = new RenderJob(world, camera, canvas, Stats);
            //for (int y = 0; y < camera.VSize; y++)
            //{
            //    for (int x = 0; x < camera.HSize; x++)
            //    {
            //        if (count >= batchSize)
            //        {
            //            RenderJobs.Enqueue(job);
            //            job = new RenderJob(world, camera, canvas, Stats);
            //            count = 0;
            //        }
            //        job.XList.Add(x);
            //        job.YList.Add(y);
            //        count++;
            //    }
            //}

            //RenderJobs.Enqueue(job);


            if (threadCount < 2)
            {
                RunTask();
                return;
            }

            threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                Thread t = new Thread(RunTask) { Name = $"worker_{i}" };
                t.Start();
                threads[i] = t;
            }

            Console.WriteLine($"Created {threads.Length} workers");
        }

        public void Wait()
        {
            if (threads == null) return;
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
