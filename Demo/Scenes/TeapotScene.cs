using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Demo.Scenes
{
    public class TeapotScene : AbstractScene
    {
        public override void Initialize()
        {
            IShape floor = new Plane
            {
                Material = new Material(new CheckerPattern(Color._Gray, Color._White))
            };
            Add(floor);

            var assembly = typeof(TeapotScene).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream("Demo.Scenes.teapot.obj");
            Group teapot = new ObjFileParser(resource);
            Add(teapot);
            Light(15, 15, -15);
            SetCameraSettings(new CameraSettings
            {
                RendererParameters = new RendererParameters { Height = 100, Width = 200 },
                ViewFrom = Tuple.Point(2, 4, -10),
                ViewTo = Tuple.Vector(0, 2, 0)
            });
        }
    }
}
