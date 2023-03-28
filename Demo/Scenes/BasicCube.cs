using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scenes
{
    public class BasicCube : AbstractScene
    {
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(new CheckerPattern(Color._White * 0.35, Color._White * 0.65), reflective: 0.4, specular: 0);

            var c1 = new Cube { Material = new Material(new Color(1, 0.3, 0.2), specular: 0.4, shininess: 5), Transform = Matrix.Transformation().Translation(2, 1, 3).Apply() };
            var c2 = new Cube { Material = new Material(new Color(0.2, 1, 0.3), specular: 0.4, shininess: 5), Transform = Matrix.Transformation().Translation(-1, 1, 2).Apply() };

            Add(new[] { floor, c1, c2});
            Light(-4.9, 4.9, -1);

            SetCameraSettings(CameraSettings.ViewFromRight());
        }
    }
}
