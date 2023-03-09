using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RayTracer
{
    public class Color
    {
        public static Color Black = new Color(0, 0, 0);

        public Color(double r, double g, double b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        //public bool Equals([AllowNull] Color other)
        //{
        //    if (other == null) return false;
        //    if (ReferenceEquals(this, other)) return true;

        //    return Red.DoubleEqual(other.Red) && Green.DoubleEqual(other.Green) && Blue.DoubleEqual(other.Blue);
        //}

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

        private int Normalized(double val)
        {
            if (val < 0) return 0;
            if (val > 1) return 255;
            return (int)Math.Round(255 * val);
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
