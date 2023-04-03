using RayTracer;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Demo.Scenes
{
    public class HeagonScene : AbstractScene
    {
        private IShape Corner()
        {
            var corner = new Sphere().Scale(0.25).Translate(tz: -1);
            return corner;
        }
        private IShape Edge()
        {
            var edge = new Cylinder
            {
                Minimum = 0,
                Maximum = 1,
            }
            .Scale(0.25, 1, 0.25).Rotate(rz: -Math.PI / 2).Rotate(ry: -Math.PI / 6).Translate(tz: -1);

            return edge;
        }
        private IShape Side()
        {
            var g = new Group();
            g.Add(Corner());
            g.Add(Edge());
            return g;
        }

        public override void Initialize()
        {

            var hex = new Group();
            for (int i = 0; i <= 5; i++)
            {
                var side = Side();
                side.Transform = Matrix.Transformation().RotateY(i * Math.PI / 3).Apply();
                hex.Add(side);
            }

            Add(hex);

            SetCameraSettings(new CameraSettings { ViewFrom = Tuple.Point(-4, 4, 0), RendererParameters = new RendererParameters { Height = 800, Width = 1200 } });
        }
    }
}
