namespace RayTracer
{
    public class IntersectionState
    {
        public IntersectionState(Intersection intersection,Ray ray)
        {
            T = intersection.T;
            Obj = intersection.Object;
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