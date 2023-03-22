using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class World : IWorld
    {
        public World()
        {
            Shapes = new List<IShape>();
        }

        public ILight Light { get; private set; }

        public List<IShape> Shapes { get; private set; }

        public void AddShape(IShape s)
        {
            Shapes.Add(s);
        }

        public void SetLight(ILight light)
        {
            Light = light;
        }

        public void Intersect(Ray ray, Intersections intersections)
        {
            var origin = ray.Origin;
            var direction = ray.Direction;
            for (var i = 0; i < Shapes.Count; i++)
            {
                var shape = Shapes[i];
                shape.Intersect(ref origin, ref direction, intersections);
            }

            intersections.Sort();
        }

        public Color ShadeHits(IntersectionState comps, int remaining = 5)
        {
            var overpoint = comps.OverPoint;
            var eyeV = comps.EyeV;
            var normalV = comps.NormalV;

            var shadowed = IsShadowed(comps.OverPoint);
            var surface = comps.Object.Material.Lighting(comps.Object, Light, overpoint, eyeV, normalV, shadowed);
            var reflected = ReflectedColor(comps, remaining);
            var refracted = RefractedColor(comps, remaining);

            var material = comps.Object.Material;
            if (material.Reflective > 0 && material.Transparency > 0)
            {
                var reflectance = comps.Schlick();
                return surface + reflected * reflectance + refracted * (1 - reflectance);
            }

            return surface + reflected + refracted;
        }

        public Color ColorAt(Ray ray, int remaining = 5)
        {
            var intersections = new Intersections();

            Intersect(ray, intersections);

            var hit = intersections.Hit();
            if (hit == null)
                return Color.Black;

            var comp = hit.PrepareComputations(ray, intersections);
            return ShadeHits(comp, remaining);
        }

        public bool IsShadowed(Tuple point)
        {
            var v = Light.Position - point;
            var distance = v.Magnitude;
            var direction = v.Normalize();

            var r = new Ray(point, direction);

            var intersections = new Intersections();
            Intersect(r, intersections);
            var h = intersections.Hit();
            if (h != null && h.T < distance)
            {
                return true;
            }

            return false;
        }

        public Color RefractedColor(IntersectionState comps, int remaining = 5)
        {
            if (remaining < 1) return Color.Black;

            if (comps.Object.Material.Transparency < Constants.Epsilon) return Color.Black;

            var nRatio = comps.N1 / comps.N2;
            var normalV = comps.NormalV;
            var cosI = comps.EyeV.Dot(normalV);
            var sin2T = nRatio * nRatio * (1 - cosI * cosI);
            if (sin2T > 1)
            {
                return Color.Black;
            }

            var cosT = Math.Sqrt(1.0 - sin2T);
            var direction = comps.NormalV * (nRatio * cosI - cosT) - comps.EyeV * nRatio;
            var refractedRay = new Ray(comps.UnderPoint, direction);
            return ColorAt(refractedRay, remaining - 1) * comps.Object.Material.Transparency;
        }

        public Color ReflectedColor(IntersectionState comps, int remaining = 5)
        {
            var materialReflective = comps.Object.Material.Reflective;
            if (remaining < 1 || materialReflective < Constants.Epsilon) return Color.Black;

            if (comps.Object.Material.Ambient == 1)
                return Color.Black;

            var reflectedRay = new Ray(comps.OverPoint, comps.ReflectV);
            var color = ColorAt(reflectedRay, remaining - 1);
            return color * comps.Object.Material.Reflective;
        }

    }
}
