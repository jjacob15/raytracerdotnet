using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RayTracer
{
    public readonly struct Color
    {
        public static Color _Black = new Color(0, 0, 0);
        public static Color _White = new Color(1, 1, 1);
        public static Color _Gray = FromRGB(211, 211, 211);
        public static Color _LightBrown = FromRGB(255, 204, 153);
        public static Color _Brown = FromRGB(204, 102, 0);
        public static Color _Purple = FromRGB(204, 0, 204);
        public static Color _Red = FromRGB(255, 0, 0);
        public static Color _Green = FromRGB(0, 255, 0);
        public static Color _Blue = FromRGB(0, 0, 255);

        public Color(double r, double g, double b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public double Red { get; }
        public double Green { get; }
        public double Blue { get; }

        public static Color operator +(Color a, Color b)
        {
            return new Color(a.Red + b.Red,
                a.Green + b.Green,
                a.Blue + b.Blue);
        }

        public static Color operator -(Color a, Color b)
        {
            return new Color(a.Red - b.Red,
                a.Green - b.Green,
                a.Blue - b.Blue);
        }

        public static Color operator *(Color a, Color b)
        {
            return new Color(a.Red * b.Red,
                a.Green * b.Green,
                a.Blue * b.Blue);
        }

        public static Color operator *(Color a, double b)
        {
            return new Color(a.Red * b,
               a.Green * b,
               a.Blue * b);
        }
        public string[] ToStringArr()
        {
            return new string[] { Normalized(Red).ToString(), Normalized(Green).ToString(), Normalized(Blue).ToString() };
        }

        public static Color FromRGB(int r, int g, int b)
        {
            return new Color(r / 255.0, g / 255.0, b / 255.0);
        }
        public static int Normalized(double val)
        {
            if (double.IsNaN(val))
            {
                return 0;
            }
            var d = Math.Min(val, 1);
            d = Math.Max(d, 0);

            return (int)Math.Round(d * 255, 0);
        }

        public override bool Equals(object obj)
        {
            return obj is Color color &&
                   Red.DoubleEqual(color.Red) &&
                   Green.DoubleEqual(color.Green) &&
                   Blue.DoubleEqual(color.Blue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue);
        }
    }
}
