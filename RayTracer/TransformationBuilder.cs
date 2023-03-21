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
            result = Matrix.Identity;
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
            var transform = Matrix.Identity;
            transform[0, 0] = x;
            transform[1, 1] = y;
            transform[2, 2] = z;

            chain.Push(transform);
            return this;
        }

        public TransformationBuilder Translation(double x, double y, double z)
        {
            var transform = Matrix.Identity;

            transform[0, 3] = x;
            transform[1, 3] = y;
            transform[2, 3] = z;

            chain.Push(transform);
            return this;
        }

        public TransformationBuilder RotateX(double r)
        {
            var transform = Matrix.Identity;
            transform[1, 1] = Math.Cos(r);
            transform[1, 2] = -Math.Sin(r);
            transform[2, 1] = Math.Sin(r);
            transform[2, 2] = Math.Cos(r);

            chain.Push(transform);
            return this;
        }

        public TransformationBuilder RotateY(double r)
        {
            var transform = Matrix.Identity;
            transform[0, 0] = Math.Cos(r);
            transform[0, 2] = Math.Sin(r);
            transform[2, 0] = -Math.Sin(r);
            transform[2, 2] = Math.Cos(r);

            chain.Push(transform);
            return this;
        }

        public TransformationBuilder RotateZ(double r)
        {
            var transform = Matrix.Identity;
            transform[0, 0] = Math.Cos(r);
            transform[0, 1] = -Math.Sin(r);
            transform[1, 0] = Math.Sin(r);
            transform[1, 1] = Math.Cos(r);

            chain.Push(transform);
            return this;
        }

        public TransformationBuilder Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            var transform = Matrix.Identity;
            transform[0, 1] = xy;
            transform[0, 2] = xz;
            transform[1, 0] = yx;
            transform[1, 2] = yz;
            transform[2, 0] = zx;
            transform[2, 1] = zy;

            chain.Push(transform);
            return this;
        }
    }
}
