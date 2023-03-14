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

        public Color ShadeHits(IntersectionState comps, int remaining = 4)
        {
            var shadowed = IsShadowed(comps.OverPoint);
            var surface = comps.Obj.Material.Lighting(comps.Obj, Light, comps.OverPoint, comps.EyeV, comps.NormalV, shadowed);
            var reflected = ReflectedColor(comps, remaining);
            var refracted = RefractedColor(comps, remaining);
            return surface + reflected + refracted ;
        }

        public Color ColorAt(Ray ray, int remaining = 4)
        {
            var xs = Intersect(ray);
            if (xs.Hit() == null)
                return Color.Black;
            else
            {
                var comp = xs.Hit().PrepareComputations(ray, xs);
                return ShadeHits(comp, remaining);
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

        public Color RefractedColor(IntersectionState comps, int remaining = 4)
        {
            if (remaining < 1) return Color.Black;

            if (comps.Obj.Material.Transparency == 0) return Color.Black;

            var nRatio = comps.N1 / comps.N2;
            var cosI = comps.EyeV.Dot(comps.NormalV);
            var sin2T = nRatio * nRatio * (1 - cosI * cosI);
            if (sin2T > 1)
            {
                return Color.Black;
            }

            var cosT = Math.Sqrt(1.0 - sin2T);
            var direction = comps.NormalV * (nRatio * cosI - cosT) - comps.EyeV * nRatio;
            var refractedRay = new Ray(comps.UnderPoint, direction);
            return ColorAt(refractedRay, remaining - 1) * comps.Obj.Material.Transparency;
        }
        public Color ReflectedColor(IntersectionState comps, int remaining = 4)
        {
            if (remaining < 1) return Color.Black;

            if (comps.Obj.Material.Ambient == 1)
                return Color.Black;
            var reflectedRay = new Ray(comps.OverPoint, comps.ReflectV);
            var color = ColorAt(reflectedRay, remaining - 1);
            return color * comps.Obj.Material.Reflective;
        }
    }
}
