using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class SmoothTriangle : Triangle
    {
        public SmoothTriangle(
            Tuple p1, Tuple p2, Tuple p3,
            Tuple n1, Tuple n2, Tuple n3
            ) : base(p1, p2, p3)
        {
            N1 = n1;
            N2 = n2;
            N3 = n3;
        }

        public Tuple N1 { get; }
        public Tuple N2 { get; }
        public Tuple N3 { get; }
    }
}
