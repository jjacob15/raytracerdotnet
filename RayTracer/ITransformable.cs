using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public interface ITransformable
    {
        Matrix Transform { get; set; }
    }
}
