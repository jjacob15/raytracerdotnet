namespace RayTracer
{
    public class IntersectionState
    {
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

            ComputeInside();
        }

        private void ComputeInside()
        {
            if (NormalV.Dot(EyeV) < 0)
            {
                Inside = true;
                NormalV = -NormalV;
            }
        }

        public double T { get; }
        public Sphere Obj { get; }
        public Tuple Point { get; }
        public Tuple EyeV { get; }
        public Tuple NormalV { get; set; }
        public bool Inside { get; set; }
    }
}