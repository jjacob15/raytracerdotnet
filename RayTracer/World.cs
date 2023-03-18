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

        //public static World DefaultWorld()
        //{
        //    World w = new World();

        //    w.Shapes.Add(new Sphere
        //    {
        //        Material = new Material(new Color(0.8, 1.0, 0.6), 0.1, 0.7, 0.2, 200)
        //    });

        //    w.Shapes.Add(new Sphere
        //    {
        //        Transform = Matrix.Identity().Scaling(0.5, 0.5, 0.5).Apply()
        //    });

        //    w.Light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1));
        //    return w;
        //}

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

        public Intersections Intersect(ref Tuple origin, ref Tuple direction)
        {
            List<Intersection> intersections = new List<Intersection>();

            for (var i = 0; i < Shapes.Count; i++)
            {
                intersections.AddRange(Shapes[i].Intersect(ref origin, ref direction));
            }
            return new Intersections(intersections);
        }

        public void Intersect(Ray ray, Intersections intersections)
        {

            for (var i = 0; i < Shapes.Count; i++)
            {
                intersections.AddRange(Shapes[i].Intersect(ray));
            }

            intersections.Sort();
        }

        public Color ShadeHits(IntersectionState comps, int remaining = 4)
        {
            var overpoint = comps.OverPoint;
            var eyeV = comps.EyeV;
            var normalV = comps.NormalV;

            var shadowed = IsShadowed(comps.OverPoint);
            var surface = comps.Object.Material.Lighting(comps.Object, Light, ref overpoint, ref eyeV, ref normalV, shadowed);
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

        public Color ShadeHitsNew(IntersectionState comps, int remaining = 4)
        {
            var overpoint = comps.OverPoint;
            var eyeV = comps.EyeV;
            var normalV = comps.NormalV;

            var shadowed = IsShadowedNew(comps.OverPoint);
            var surface = comps.Object.Material.Lighting(comps.Object, Light, ref overpoint, ref eyeV, ref normalV, shadowed);
            var reflected = ReflectedColorNew(comps, remaining);
            var refracted = RefractedColorNew(comps, remaining);

            var material = comps.Object.Material;
            if (material.Reflective > 0 && material.Transparency > 0)
            {
                var reflectance = comps.Schlick();
                return surface + reflected * reflectance + refracted * (1 - reflectance);
            }

            return surface + reflected + refracted;
        }

        public Color ColorAt(Ray ray, int remaining = 4)
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
            var distance = v.Magnitude();
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

        public bool IsShadowedNew(Tuple point)
        {
            var v = Light.Position - point;
            var distance = v.Magnitude();
            var direction = v.Normalize();

            var intersections = Intersect(ref point, ref direction);
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

            if (comps.Object.Material.Transparency == 0) return Color.Black;

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
            return ColorAt(refractedRay, remaining - 1) * comps.Object.Material.Transparency;
        }

        public Color RefractedColorNew(IntersectionState comps, int remaining = 4)
        {
            if (remaining < 1) return Color.Black;

            if (comps.Object.Material.Transparency == 0) return Color.Black;

            var nRatio = comps.N1 / comps.N2;
            var cosI = comps.EyeV.Dot(comps.NormalV);
            var sin2T = nRatio * nRatio * (1 - cosI * cosI);
            if (sin2T > 1)
            {
                return Color.Black;
            }

            var cosT = Math.Sqrt(1.0 - sin2T);
            var direction = comps.NormalV * (nRatio * cosI - cosT) - comps.EyeV * nRatio;
            //var refractedRay = new Ray(comps.UnderPoint, direction);
            var overPoint = comps.UnderPoint;
            //var reflectv = comps.ReflectV;
            return ColorAtNew(ref overPoint, ref direction, remaining - 1) * comps.Object.Material.Transparency;
        }

        public Color ReflectedColorNew(IntersectionState comps, int remaining = 4)
        {
            if (remaining < 1) return Color.Black;

            if (comps.Object.Material.Ambient == 1)
                return Color.Black;

            var overPoint = comps.OverPoint;
            var reflectv = comps.ReflectV;
            var color = ColorAtNew(ref overPoint, ref reflectv, remaining - 1);
            return color * comps.Object.Material.Reflective;
        }

        public Color ReflectedColor(IntersectionState comps, int remaining = 4)
        {
            if (remaining < 1) return Color.Black;

            if (comps.Object.Material.Ambient == 1)
                return Color.Black;

            var reflectedRay = new Ray(comps.OverPoint, comps.ReflectV);
            var color = ColorAt(reflectedRay, remaining - 1);
            return color * comps.Object.Material.Reflective;
        }

        public Color ColorAtNew(ref Tuple origin, ref Tuple direction, int remaining = 4)
        {
            var xs = Intersect(ref origin, ref direction);
            if (xs.Hit() == null)
                return Color.Black;
            else
            {
                var comp = xs.Hit().PrepareComputations(ref origin, ref direction, xs);
                return ShadeHitsNew(comp, remaining);
            }
        }
    }
}
