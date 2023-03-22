using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class TransformationBuilder
    {
        private Stack<Matrix> chain;
        private Matrix result;

        public TransformationBuilder()
        {
            chain = new Stack<Matrix>();
            result = new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }
        public Matrix Apply()
        {
            while (chain.Count > 0)
            {
                var transform = chain.Pop();
                result = result * transform;
            }
            return result;
        }

        public TransformationBuilder Scaling(double x, double y, double z)
        {
            chain.Push(new Matrix(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1
            ));
            return this;
        }

        public TransformationBuilder Translation(double x, double y, double z)
        {
            chain.Push(new Matrix(
                 1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1
             ));
            return this;
        }

        public TransformationBuilder RotateX(double a)
        {
            chain.Push(new Matrix(
               1, 0, 0, 0,
                0, Math.Cos(a), -Math.Sin(a), 0,
                0, Math.Sin(a), Math.Cos(a), 0,
                0, 0, 0, 1
            ));
            return this;
        }

        public TransformationBuilder RotateY(double a)
        {
            chain.Push(new Matrix(
               Math.Cos(a), 0, Math.Sin(a), 0,
                0, 1, 0, 0,
                -Math.Sin(a), 0, Math.Cos(a), 0,
                0, 0, 0, 1
           ));
            return this;
        }

        public TransformationBuilder RotateZ(double a)
        {
            chain.Push(new Matrix(
                Math.Cos(a), -Math.Sin(a), 0, 0,
                Math.Sin(a), Math.Cos(a), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
           ));
            return this;
        }

        public TransformationBuilder Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            chain.Push(new Matrix(
                1, xy, xz, 0,
                yx, 1, yz, 0,
                zx, zy, 1, 0,
                0, 0, 0, 1
           ));
            return this;
        }
    }
}
