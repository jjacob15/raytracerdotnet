using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Demo.Scenes
{
    public class CsgDiceScene : AbstractScene
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
            .Scale(0.25, 1, 0.25).Rotate(rz: -Math.PI / 2).Rotate(ry: -Math.PI / 6)
            .Translate(tx: -0.5, tz: -1);

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
            IShape floor = new Plane();
            floor.Material = new Material(new CheckerPattern(Color._White * 0.35, Color._White * 0.65), reflective: 0.4, specular: 0);
            floor.Translate(ty: -0.5);
            //Add(floor);

            Light(5, 15, -5);

            var bottom = new Group();
            for (int i = 0; i <= 3; i++)
            {
                var side = Side();
                side.Transform = Matrix.Transformation().RotateY(i * Math.PI / 2).Apply();
                bottom.Add(side);
            }


            Add(bottom);
            //var cube = new Cube();
            //cube.Material = new Material(Color._LightBlue);
            //cube.Translate(0, 1, 1);

            //var sphere = new Sphere();
            //sphere.Material = new Material(Color._White);
            //sphere.Scale(0.25);
            //sphere.Translate(0, 1, -0.15);

            //Add(new Csg(CsgOperation.Difference, cube, sphere));
            //SetCameraSettings(new CameraSettings { ViewFrom = Tuple.Point(2, 3, -5), ViewTo = Tuple.Point(0, 1, 0) });
            SetCameraSettings(new CameraSettings { ViewFrom = Tuple.Point(0, 5, -5), ViewTo = Tuple.Point(0, 1, 0) });
        }
    }
}
