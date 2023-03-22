using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scenes
{
    public class ReflectionScene : AbstractScene
    {
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(new CheckerPattern(Color.White, new Color(0.9, 0.9, 0.9)), specular: 0, reflective: 0);
            floor.Transform = Matrix.Transformation().Translation(0, 0, 0).Apply();

            var middle = new Sphere();
            middle.Transform = Matrix.Transformation().Translation(0, 1, 0.5).Apply();
            middle.Material = new Material(new Color(0.8, 1, 0.1), diffuse: 0.7, specular: 0.3, reflective: 1);

            var right = new Sphere();
            right.Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Translation(1.5, 0.5, -0.5).Apply();
            right.Material = new Material(new GradientPattern(new Color(0, 0, 1), new Color(1, 0.5, 0.1))
            { Transform = Matrix.Transformation().Scaling(0.25, 1, 1).RotateY(180).Apply() },
            diffuse: 0.7, specular: 1, reflective: 1);

            var left = new Sphere();
            left.Transform = Matrix.Transformation().Scaling(0.33, 0.33, 0.33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material = new Material(new CheckerPattern(new Color(1, 0, 0), new Color(0, 1, 0))
            { Transform = Matrix.Transformation().Scaling(0.25, 0.25, 0.25).Apply() }, diffuse: 0.7,
            specular: 0.3,
            reflective: 1);

            Add(new[] {
                floor,middle, left, right });
        }
    }
}
