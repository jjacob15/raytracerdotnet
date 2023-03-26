using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scenes
{
    public class ReflectionRefractionScene : AbstractScene
    {
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(new CheckerPattern(Color.White * 0.35, Color.White * 0.65)
            {
                Transform = Matrix.Transformation().RotateX(45).Apply()
            }, reflective: 0.4, specular: 0);

            var s1 = new Sphere { Material = new Material(new Color(1, 0.3, 0.2), specular: 0.4, shininess: 5), Transform = Matrix.Transformation().Translation(2, 1, 3).Apply() };
            var s2 = new Sphere { Material = new Material(new Color(0.2, 1, 0.3), specular: 0.4, shininess: 5), Transform = Matrix.Transformation().Translation(-1, 1, 2).Apply() };

            var blueSphere = new Sphere
            {
                Material = new Material(new Color(0, 0, 0.2), ambient: 0, diffuse: 0.4, specular: 0.9, shininess: 300, reflective: 0.9, transparency: 0.9, refractiveIndex: 1.5),
                Transform = Matrix.Transformation().Scaling(0.7, 0.7, 0.7).Translation(0.6, 1, -0.6).Apply()
            };
            var greenSphere = new Sphere
            {
                Material = new Material(new Color(0, 0.2, 0), ambient: 0, diffuse: 0.4, specular: 0.9, shininess: 300, reflective: 0.9, transparency: 0.9, refractiveIndex: 1.5),
                Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Translation(-0.7, 1, -0.8).Apply()
            };

            Add(new[] { floor, s1, s2, blueSphere, greenSphere });
            Light(-4.9, 4.9, -1);
        }
    }
}
