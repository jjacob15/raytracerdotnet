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
            var corner = new Sphere
            {
                Transform = Matrix.Transformation().Scaling(0.25, 0.25, 0.25).Translation(0, 0, -1).Apply()
            };
            return corner;
        }
        private IShape Edge()
        {
            var edge = new Cylinder
            {
                Minimum = 0,
                Maximum = 1,
                Transform = Matrix.Transformation()
                .Scaling(0.25, 1, 0.25)
                .RotateZ(-Math.PI / 2)
                .RotateY(-Math.PI / 6)
                .Translation(0, 0, -1)
                .Apply()
            };

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

            SetCameraSettings(new CameraSettings { ViewFrom = Tuple.Point(-4, 4, 0) });
        }
    }
}
