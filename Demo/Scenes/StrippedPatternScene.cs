using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scenes
{
    public class StrippedPatternScene : Scene
    {
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material.Pattern = new RingPattern(Color.White, new Color(0, 0, 1));
            floor.Material.Specular = 0;
            Add(floor);

            var middle = new Sphere();
            middle.Material.Pattern = new StrippedPattern(new Color(0.5, 1, 0.1), new Color(1, 0.5, 0.1));
            middle.Transform = Matrix.Identity().Translation(-0.5, 1, 0.5).Apply(); ;
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            Add(middle);

            var right = new Sphere();
            right.Material.Pattern = new GradientPattern(new Color(0, 0, 1), new Color(1, 0.5, 0.1));
            right.Transform = Matrix.Identity().Translation(-1.8, 2.5, -1.5).Scaling(0.25, .25, .25).RotateY(30).Apply();
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 1;
            Add(right);

            var left = new Sphere();
            left.Transform = Matrix.Identity().Scaling(0.33, 0.33, 0.33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material.Pattern = new CheckerPattern(new Color(1, 0, 0), new Color(0, 1, 0));
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            Add(left);
        }
    }
}
