using Demo.Scenes;
using RayTracer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //GCSettings.LatencyMode = GCLatencyMode.Batch;

            var threading = true;
            int threadCount = threading ? (int) Environment.ProcessorCount :1;
            var display = true;

            List<Type> scenes = new List<Type> {
                //    typeof(SimpleScene),
                //typeof(StrippedPatternScene),
                //typeof(ReflectionScene),
                //typeof(ReflectionRefractionScene),
                //typeof(BasicCube),
                //typeof(TableScene),
                //typeof(HeagonScene)
                //typeof(CsgScene),
                typeof(CsgDiceScene),
                //typeof(CsgDifferenceScene),
                //typeof(TeapotScene)
            };

            string dir = Path.Combine(Path.GetTempPath(), "raytracing");
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }

            Directory.CreateDirectory(dir);

            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine($"Start time: {DateTime.Now:HH:mm:ss}");
            var files = Run(dir, threadCount, scenes.ToArray());
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds:###,###,##0} ms");

            if (display)
                Display.OpenPicture(files.Count == 1 ? files[0] : dir);
        }

        private static List<string> Run(string dir, int threadCount, Type[] scenes)
        {
            var files = new List<string>();
            Renderer renderer = new Renderer(dir);
            foreach (var scene in scenes)
            {
                Timer timer = new Timer(500)
                {
                    AutoReset = true
                };
                timer.Elapsed += (sender, args) => { ConsoleStats(scene.Name, renderer); };
                timer.Start();

                renderer.Render(scene,  threadCount);
                renderer.Wait();

                timer.Stop();
                Console.WriteLine();

                var file = renderer.Save($"{scene.Name}.ppm");
                files.Add(file);
            }

            return files;

        }

        private static void ConsoleStats(string name, Renderer renderer)
        {
            Console.CursorLeft = 0;
            var stats = renderer.Stats;
            if (stats == null)
            {
                return;
            }
            Console.Write($"{name} {stats}");
        }
    }
}
