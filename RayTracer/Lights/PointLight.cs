using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Lights
{
    public class PointLight
    {
        public PointLight(Tuple position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public Tuple Position { get; }
        public Color Intensity { get; }

        public override bool Equals(object obj)
        {
            return obj is PointLight light &&
                   EqualityComparer<Tuple>.Default.Equals(Position, light.Position) &&
                   EqualityComparer<Color>.Default.Equals(Intensity, light.Intensity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Intensity);
        }
    }
}
