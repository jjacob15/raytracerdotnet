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
        public Material(IPattern pattern, double ambient = 0.1, double diffuse = 0.9, double specular = 0.9, int shininess = 200,
            double reflective = 0.0, double transparency = 0, double refractiveIndex = 1) : this(Color._White)
        {
            Pattern = pattern;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = transparency;
            RefractiveIndex = refractiveIndex;
        }

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

        public Color Color { get; }
        public double Ambient { get; set; } = 0.1;
        public double Diffuse { get; set; } = 0.9;
        public double Specular { get; set; } = 0.9;
        public double Shininess { get; set; } = 200;
        public double Reflective { get; set; } = 0.0;
        public double Transparency { get; set; } = 0;
        public double RefractiveIndex { get; set; } = 1;

        public IPattern Pattern { get; set; }

        public Color Lighting(IShape shape, ILight light, ref Tuple point, ref Tuple eyeV, ref Tuple normalV, bool inShadow)
        {
            Color newColor = Color;
            if (!ReferenceEquals(Pattern, null))
            {
                newColor = Pattern.PatternAtShape(shape, ref point);
                //combine the surface color with the light's color/intensity
            }
            return Lighting(light, ref point, ref eyeV, ref normalV, inShadow, ref newColor);
        }

        private Color Lighting(ILight light, ref Tuple point, ref Tuple eyeV, ref Tuple normalV, bool inShadow, ref Color color)
        {
            var effectiveColor = color * light.Intensity;
            //find the direction to the light source
            var lightV = (light.Position - point).Normalize();
            //compute the ambient contribution
            var ambient = effectiveColor * Ambient;
            ///TODO
            ///remove dot and manuallty multiple to save copy
            var lightDotNormal = lightV.Dot(normalV);

            //light_dot_normal represents the cosine of the angle between the
            //light vector and the normal vector. A negative number means the
            //light is on the other side of the surface.


            Color diffuse = Color._Black;
            Color specular = Color._Black;
            if (lightDotNormal > 0 && !inShadow)
            {
                // compute the diffuse contribution
                diffuse = effectiveColor * Diffuse * lightDotNormal;
                // reflect_dot_eye represents the cosine of the angle between the
                // reflection vector and the eye vector. A negative number means the
                // light reflects away from the eye.
                var reflectV = -lightV.Reflect(normalV);
                var reflectEyeDot = reflectV.Dot(eyeV);

                if (reflectEyeDot > 0)
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
