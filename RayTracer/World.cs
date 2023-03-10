using RayTracer.Lights;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class World
    {
        public World()
        {
            Shapes = new List<Sphere>();
        }

        public static World DefaultWorld()
        {
            World w = new World();

            w.Shapes.Add(new Sphere
            {
                Material = new Material(new Color(0.8, 1.0, 0.6), 0.1, 0.7, 0.2, 200)
            });

            w.Shapes.Add(new Sphere
            {
                Transform = Matrix.Identity().Scaling(0.5, 0.5, 0.5).Apply()
            });

            w.Light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1));
            return w;
        }

        public PointLight Light { get; set; }

        public List<Sphere> Shapes { get; set; }

        public Intersections Intersect(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();

            foreach (Sphere shape in Shapes)
                intersections.AddRange(shape.Intersect(ray));

            return new Intersections(intersections);
        }

        public Color ShadeHits(IntersectionState comps)
        {
            return comps.Obj.Material.Lighting(Light, comps.Point, comps.EyeV, comps.NormalV);
        }

        public Color ColorAt(Ray ray)
        {
            var xs = Intersect(ray);
            if (xs.Hit() == null)
                return Color.Black;
            else
            {
                var comp = xs.Hit().PrepareComputations(ray);
                return ShadeHits(comp);
            }
        }
    }
}
