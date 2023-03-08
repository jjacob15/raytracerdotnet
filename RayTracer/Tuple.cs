using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracer
{
    public class Tuple
    //: IEquatable<Tuple>
    {
        public static Tuple Zero() => new Tuple(0, 0, 0, 0);

        public static Tuple Point(double x, double y, double z) => new Tuple(x, y, z, 1);
        public static Tuple Vector(double x, double y, double z) => new Tuple(x, y, z, 0);

        public Tuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; set; }

        public bool IsPoint => W == 1;
        public bool IsVector => W == 0;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            Tuple tuple = (Tuple)obj;
            return this == tuple;
        }

        public override int GetHashCode()
        {
            double hashcode = 23;
            hashcode = (hashcode * 37) + X;
            hashcode = (hashcode * 37) + Y;
            hashcode = (hashcode * 37) + Z;
            return (int) Math.Round((hashcode * 37) + W);
        }

        public static bool operator ==(Tuple a, Tuple b)
        {
            return a.X.DoubleEqual(b.X) &&
                a.Y.DoubleEqual(b.Y) &&
                a.Z.DoubleEqual(b.Z) &&
                a.W.DoubleEqual(b.W);
        }

        public static bool operator !=(Tuple a, Tuple b)
        {
            return !(a == b);
        }

        public static Tuple operator +(Tuple a, Tuple b)
        {
            return new Tuple(a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z,
                Math.Max(a.W, b.W));
        }

        public static Tuple operator -(Tuple a, Tuple b)
        {
            return new Tuple(a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z,
                Math.Abs(a.W - b.W));
        }

        public static Tuple operator -(Tuple a) => new Tuple(-a.X, -a.Y, -a.Z, -a.W);

        public static Tuple operator --(Tuple a)
        {
            return new Tuple(-a.X, -a.Y, -a.Z, -a.W);
        }

        public static Tuple operator /(Tuple a, double divisor)
        {
            return new Tuple(a.X / divisor, a.Y / divisor, a.Z / divisor, a.W / divisor);
        }

        public static Tuple operator *(Tuple a, double multiplier)
        {
            return new Tuple(a.X * multiplier, a.Y * multiplier, a.Z * multiplier, a.W * multiplier);
        }

        public double Magnitude()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2) + Math.Pow(W, 2));
        }

        public Tuple Normalize()
        {
            var m = Magnitude();
            return new Tuple(X / m, Y / m, Z / m, W / m);
        }

        public double Dot(Tuple t)
        {
            return X * t.X + Y * t.Y + Z * t.Z + W * t.W;
        }

        public Tuple Cross(Tuple t)
        {
            if (IsPoint || t.IsPoint) throw new NotSupportedException("Cross has to be between two vectors");

            return Vector(Y * t.Z - Z * t.Y,
                Z * t.X - X * t.Z,
                X * t.Y - Y * t.X);

        }

        public override string ToString()
        {
            return $"x {Math.Round(X,2)} y {Math.Round(Y, 2)} z {Math.Round(Z, 2)} w {Math.Round(W, 2)}";
        }

        public Tuple Reflect(Tuple normal)
        {
            return this - normal * 2 * Dot(normal);
        }
    }
}
