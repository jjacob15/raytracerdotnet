using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Demo.Scenes
{
    public class CsgScene : AbstractScene
    {
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(new CheckerPattern(Color._White * 0.35, Color._White * 0.65), reflective: 0.4, specular: 0);
            Add(floor);

            Light(5, 15, -5);

            var cube = new Cube();
            var sphere = new Sphere().Scale(1.4);
            var csgIntersection = new Csg(CsgOperation.Intersect, sphere, cube);

            var cyl1 = new Cylinder(-4, 4) { Material = new Material(Color._Red) }.Scale(sx: 0.75, sz: 0.75);
            var cyl2 = new Cylinder(-4, 4) { Material = new Material(Color._Green) }.Scale(sx: 0.75, sz: 0.75).Rotate(rx: Math.PI / 2);
            var cyl3 = new Cylinder(-4, 4) { Material = new Material(Color._Blue) }.Scale(sx: 0.75, sz: 0.75).Rotate(rz: Math.PI / 2);
            var csgUnion = new Csg(CsgOperation.Union, cyl1, new Csg(CsgOperation.Union, cyl2, cyl3));

            var csgDifference = new Csg(CsgOperation.Difference, csgIntersection, csgUnion);
            csgDifference.Translate(ty: 1).Rotate(ry: Math.PI / 4);
            Add(csgDifference);

            SetCameraSettings(new CameraSettings { ViewFrom = Tuple.Point(0, 3, -5), ViewTo = Tuple.Point(0, 1, 0) });
        }
    }
}
