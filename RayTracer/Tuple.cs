﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracer
{
    public readonly struct Tuple
    {
        public static Tuple ZeroPoint() => new Tuple(0, 0, 0, 1);
        public static Tuple ZeroVector() => new Tuple(0, 0, 0, 0);

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
        public double W { get; }

        public bool IsPoint => W == 1.0d;
        public bool IsVector => W == 0.0d;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            Tuple tuple = (Tuple)obj;
            return this == tuple;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Tuple a, Tuple b) => a.X.DoubleEqual(b.X) && a.Y.DoubleEqual(b.Y) && a.Z.DoubleEqual(b.Z) && a.W.DoubleEqual(b.W);
        public static bool operator !=(Tuple a, Tuple b) => !(a == b);

        public static Tuple operator +(Tuple a, Tuple b) => new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, Math.Max(a.W, b.W));
        public static Tuple operator -(Tuple a, Tuple b) => new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, Math.Abs(a.W - b.W));
        public static Tuple operator -(Tuple a) => new Tuple(-a.X, -a.Y, -a.Z, -a.W);
        public static Tuple operator --(Tuple a) => new Tuple(-a.X, -a.Y, -a.Z, -a.W);
        public static Tuple operator /(Tuple a, double divisor) => new Tuple(a.X / divisor, a.Y / divisor, a.Z / divisor, a.W / divisor);
        public static Tuple operator *(Tuple a, double multiplier) => new Tuple(a.X * multiplier, a.Y * multiplier, a.Z * multiplier, a.W * multiplier);

        public double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);


        public Tuple Normalize()
        {
            var m = Magnitude;
            return new Tuple(X / m, Y / m, Z / m, W / m);
        }

        public double Dot(Tuple t) => X * t.X + Y * t.Y + Z * t.Z + W * t.W;
        public double Dot(ref Tuple t) => X * t.X + Y * t.Y + Z * t.Z + W * t.W;

        public Tuple Cross(Tuple t)
        {
            if (IsPoint || t.IsPoint) throw new NotSupportedException("Cross has to be between two vectors");

            return Vector(Y * t.Z - Z * t.Y,
                Z * t.X - X * t.Z,
                X * t.Y - Y * t.X);

        }

        public override string ToString() => $"X: {X} Y: {Y} Z: {Z} W: {W}";

        public Tuple Reflect(Tuple normal) => this - normal * 2 * Dot(normal);
    }
}
