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
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(new CheckerPattern(Color._White * 0.35, Color._White * 0.65), reflective: 0.4, specular: 0);
            Add(floor);

            Light(5, 15, -5);


            var cube = new Cube();
            cube.Material = new Material(Color._LightBlue);
            cube.Translate(0, 1, 1);

            var sphere = new Sphere();
            sphere.Material = new Material(Color._White);
            sphere.Scale(0.25);
            sphere.Translate(0, 1, -0.15);

            Add(new Csg(CsgOperation.Difference, cube, sphere));
            SetCameraSettings(new CameraSettings { ViewFrom = Tuple.Point(2, 3, -5), ViewTo = Tuple.Point(0, 1, 0) });
        }
    }
}
