using RayTracer.Lights;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Material
    {
        public Material(Color color, double ambient = 0.1, double diffuse = 0.9, double specular = 0.9, int shininess = 200,
            double reflective = 0.0, double transparency = 0, double refractiveIndex = 1)
        {
            Color = color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = transparency;
            RefractiveIndex = refractiveIndex;
        }

        public Material() : this(new Color(1, 1, 1))
        {

        }

        public Color Color { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public double Reflective { get; set; }
        public double Transparency { get; set; }
        public double RefractiveIndex { get; set; }

        public IPattern Pattern { get; set; }

        public Color Lighting(IShape shape, PointLight light, Tuple point, Tuple eyeV, Tuple normalV, bool inShadow)
        {
            if (Pattern != null)
            {
                Color = Pattern.PatternAtShape(shape, point);
            }
            //combine the surface color with the light's color/intensity
            var effectiveColor = Color * light.Intensity;
            //find the direction to the light source
            var lightV = (light.Position - point).Normalize();
            //compute the ambient contribution
            var ambient = effectiveColor * Ambient;
            var lightDotNormal = lightV.Dot(normalV);

            //light_dot_normal represents the cosine of the angle between the
            //light vector and the normal vector. A negative number means the
            //light is on the other side of the surface.

            Color diffuse = Color.Black;
            Color specular = Color.Black;
            if (lightDotNormal > 0 && !inShadow)
            {
                // compute the diffuse contribution
                diffuse = effectiveColor * Diffuse * lightDotNormal;
                // reflect_dot_eye represents the cosine of the angle between the
                // reflection vector and the eye vector. A negative number means the
                // light reflects away from the eye.
                var reflectV = -lightV.Reflect(normalV);
                var reflectEyeDot = reflectV.Dot(eyeV);

                if (reflectEyeDot <= 0)
                {
                    specular = Color.Black;
                }
                else
                {
                    //compute the specular contribution
                    var factor = Math.Pow(reflectEyeDot, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }
            //Add the three contributions together to get the final shading
            return ambient + diffuse + specular;
        }

        public override bool Equals(object obj)
        {
            return obj is Material material &&
                   EqualityComparer<Color>.Default.Equals(Color, material.Color) &&
                   Ambient == material.Ambient &&
                   Diffuse == material.Diffuse &&
                   Specular == material.Specular &&
                   Shininess == material.Shininess;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color, Ambient, Diffuse, Specular, Shininess);
        }
    }
}
