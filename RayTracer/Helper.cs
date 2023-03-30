using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class Helper
    {
        public static T Rotate<T>(this T shape, double rx = 0, double ry = 0, double rz = 0)
            where T : ITransformable
        {
            TransformRotate(shape, rx, ry, rz);
            return shape;
        }

        private static void TransformRotate(ITransformable transformable, double tx = 0, double ty = 0, double tz = 0)
        {
            transformable.Transform = new TransformationBuilder().RotateX(tx)
                .RotateY(ty)
                .RotateZ(tz).Apply();
        }

        public static T Translate<T>(this T shape, double tx = 0, double ty = 0, double tz = 0)
            where T : ITransformable
        {
            TransformTranslate(shape, tx, ty, tz);
            return shape;
        }

        private static void TransformTranslate(ITransformable transformable, double tx = 0, double ty = 0, double tz = 0)
        {
            transformable.Transform = new TransformationBuilder().Translation(tx, ty, tz).Apply();
        }

        public static T Scale<T>(this T shape, double scale)
          where T : ITransformable
        {
            TransformScale(shape, scale, scale, scale);
            return shape;
        }

        public static T Scale<T>(this T shape, double sx = 1, double sy = 1, double sz = 1)
            where T : ITransformable
        {
            TransformScale(shape, sx, sy, sz);
            return shape;
        }

        private static void TransformScale(ITransformable transformable, double sx = 0, double sy = 0, double sz = 0)
        {
            transformable.Transform = new TransformationBuilder().Scaling(sx, sy, sz).Apply();
        }

        public static Ray Ray(double px, double py, double pz, double vx, double vy, double vz) => new Ray(Tuple.Point(px, py, pz), Tuple.Vector(vx, vy, vz));
    }
}
