using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RayTracer
{
    public class Intersections : List<Intersection>
    {
        public Intersections(params Intersection[] intersections) : base(intersections)
        {

        }
        public Intersection Hit() => this.OrderBy(x=>x.T).FirstOrDefault(x => x.T >= 0);
    }
}
