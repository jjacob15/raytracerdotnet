using RayTracer.Shapes;

namespace RayTracer
{
    public class IntersectionState
    {
        private const double Epsilon = 1e-5;
        public IntersectionState(Intersection intersection, Ray ray)
        {
            //instantiate a data structure for storing some precomputed values

            //copy the intersection's properties, for convenience
            T = intersection.T;
            Obj = intersection.Object;

            //precompute some useful values
            Point = ray.Position(T);
            EyeV = -ray.Direction;
            NormalV = Obj.NormalAt(ray.Position(T));

            if (NormalV.Dot(EyeV) < 0)
            {
                Inside = true;
                NormalV = -NormalV;
            }


            //do not use double.Epsilon
            OverPoint = Point + NormalV * Epsilon;
        }

        public double T { get; }
        public IShape Obj { get; }
        public Tuple Point { get; }
        public Tuple OverPoint { get; }
        public Tuple EyeV { get; }
        public Tuple NormalV { get;  }
        public bool Inside { get; }
    }
}