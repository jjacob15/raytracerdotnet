using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class World
    {
        public World()
        {
            Shapes = new List<IShape>();
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

        public List<IShape> Shapes { get; private set; }

        public void AddShape(IShape s)
        {
            Shapes.Add(s);
        }

        public Intersections Intersect(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();

            foreach (IShape shape in Shapes)
                intersections.AddRange(shape.Intersect(ray));

            return new Intersections(intersections);
        }

        public Color ShadeHits(IntersectionState comps)
        {
            var shadowed = IsShadowed(comps.OverPoint);
            return comps.Obj.Material.Lighting(Light, comps.OverPoint, comps.EyeV, comps.NormalV, shadowed);
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

        public bool IsShadowed(Tuple point)
        {
            var v = Light.Position - point;
            var distance = v.Magnitude();
            var direction = v.Normalize();

            var r = new Ray(point, direction);
            var intersections = Intersect(r);
            var h = intersections.Hit();
            if (h != null && h.T < distance)
            {
                return true;
            }

            return false;
        }
    }
}
