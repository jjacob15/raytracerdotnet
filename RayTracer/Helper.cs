using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class Helper
    {
        //public static Matrix Scaling(double x, double y, double z)
        //{
        //    var identity = Matrix.Identity();
        //    identity[0, 0] = x;
        //    identity[1, 1] = y;
        //    identity[2, 2] = z;

        //    return identity;
        //}
        //public static Matrix Translation(double x, double y, double z)
        //{
        //    var identity = Matrix.Identity();
        //    identity[0, 3] = x;
        //    identity[1, 3] = y;
        //    identity[2, 3] = z;

        //    return identity;
        //}

        //public static Matrix RotateX(double r)
        //{
        //    Matrix m = Matrix.Identity();
        //    m[1, 1] = Math.Cos(r);
        //    m[1, 2] = -Math.Sin(r);
        //    m[2, 1] = Math.Sin(r);
        //    m[2, 2] = Math.Cos(r);
        //    return m;
        //}

        //public static Matrix RotateY(double r)
        //{
        //    Matrix m = Matrix.Identity();
        //    m[0, 0] = Math.Cos(r);
        //    m[0, 2] = Math.Sin(r);
        //    m[2, 0] = -Math.Sin(r);
        //    m[2, 2] = Math.Cos(r);
        //    return m;
        //}

        //public static Matrix RotateZ(double r)
        //{
        //    Matrix m = Matrix.Identity();
        //    m[0, 0] = Math.Cos(r);
        //    m[0, 1] = -Math.Sin(r);
        //    m[1, 0] = Math.Sin(r);
        //    m[1, 1] = Math.Cos(r);
        //    return m;
        //}

        //public static Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
        //{
        //    Matrix m = Matrix.Identity();
        //    m[0, 1] = xy;
        //    m[0, 2] = xz;
        //    m[1, 0] = yx;
        //    m[1, 2] = yz;
        //    m[2, 0] = zx;
        //    m[2, 1] = zy;
        //    return m;
        //}
    }
}
