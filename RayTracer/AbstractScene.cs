using RayTracer;
using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace RayTracer
{
    public abstract class AbstractScene
    {
        public IWorld World { get; }
        public CameraSettings CameraSettings { get; private set; } = new CameraSettings();

        public AbstractScene()
        {
            World = new World();
            World.SetLight(new PointLight(Tuple.Point(-10, 10, -10), Color._White));

        }

        public abstract void Initialize();

        public void Add<T>(T shape) where T : IShape
        {
            World.AddShape(shape);
        }

        public void Add(params IShape[] shapes)
        {
            foreach (var shape in shapes) Add<IShape>(shape);
        }

        public void Add(ILight light)
        {
            World.SetLight(light);
        }

        public void Light(double x, double y, double z)
        {
            var p = new PointLight(Tuple.Point(x, y, z), Color._White);
            World.SetLight(p);
        }

        public void SetCameraSettings(CameraSettings settings)
        {
            CameraSettings = settings;
        }

    }
}
