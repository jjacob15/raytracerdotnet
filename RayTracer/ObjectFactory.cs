using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class ObjectFactory
    {
        public static Ray IntersectingRay()
        {
            return new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
        }
        public static World DefaultWorld()
        {
            World w = new World();

            w.Shapes.Add(new Sphere
            {
                Material = new Material(new Color(0.8, 1.0, 0.6), diffuse: 0.7, specular: 0.2)
            });

            w.Shapes.Add(new Sphere
            {
                Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Apply()
            });

            w.SetLight(new PointLight(Tuple.Point(-10, 10, -10), Color.White));
            return w;
        }
    }
}
