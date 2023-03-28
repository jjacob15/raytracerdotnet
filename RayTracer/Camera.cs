using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class CameraSettings
    {
        public Tuple ViewFrom { get; set; } = Tuple.Point(0, 1.5, -5);
        public Tuple ViewTo { get; set; } = Tuple.Point(0, 1, 0);

        public RendererParameters RendererParameters { get; } = RendererParameters.HighQuality;

        public Camera Build()
        {
            var c = new Camera(RendererParameters.Width, RendererParameters.Height, Math.PI / 3,
             Matrix.ViewTransform(ViewFrom,
               ViewTo, Tuple.Vector(0, 1, 0)));
            return c;
        }

        public static CameraSettings ViewFromFarTopLeft()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(-13, 17, -13), ViewTo = Tuple.Vector(0, 5, 0) };
        }
        public static CameraSettings ViewFromFarLeft()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(-13, 3, -13), ViewTo = Tuple.Vector(0, 5, 0) };
        }

        public static CameraSettings ViewFromTopLeft()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(-5 , 7, -5), ViewTo = Tuple.Vector(0, 5, 0) };
        }
        public static CameraSettings ViewFromLeft()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(-5, 3, -5), ViewTo = Tuple.Vector(0, 2, 0) };
        }

        public static CameraSettings ViewFromTopRight()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(13, 17, 13), ViewTo = Tuple.Vector(0, 5, 0) };

        }
        public static CameraSettings ViewFromRight()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(13, 5, 13), ViewTo = Tuple.Vector(0, 5, 0) };
        }
        public static CameraSettings ViewFromTopCenter()
        {
            return new CameraSettings { ViewFrom = Tuple.Point(10, 10, 10), ViewTo = Tuple.Vector(0, 10, 0) };
        }
    }

    public class Camera : ICamera
    {
        //public static Camera DefaultCamera()
        //{
        //    var c = new Camera(640, 400, Math.PI / 3,
        //        Matrix.ViewTransform(Tuple.Point(0, 1.5, -5),
        //          Tuple.Point(0, 1, 0), Tuple.Vector(0, 1, 0)));
        //    return c;
        //}
        public Camera(double hSize, double vSize, double fieldOfView, Matrix transform) : this(hSize, vSize, fieldOfView)
        {
            Transform = transform;
            _transformInverse = Transform.Inverse();
        }

        public Camera(double hSize, double vSize, double fieldOfView)
        {
            HSize = hSize;
            VSize = vSize;
            FieldOfView = fieldOfView;
            ComputePixelSize();
        }

        private void ComputePixelSize()
        {
            double halfView = Math.Tan(FieldOfView / 2);
            double aspect = HSize / VSize;
            if (aspect >= 1)
            {
                HalfWidth = halfView;
                HalfHeight = halfView / aspect;
            }
            else
            {
                HalfWidth = halfView * aspect;
                HalfHeight = halfView;
            }
            PixelSize = (HalfWidth * 2) / HSize;
        }

        public double HSize { get; }
        public double VSize { get; }
        public double FieldOfView { get; }
        public Matrix Transform { get; private set; }
        private Matrix _transformInverse;

        public double PixelSize { get; private set; }
        public double HalfWidth { get; private set; }
        public double HalfHeight { get; private set; }


        public Ray RayForPixel(int px, int py)
        {
            //the offset from the edge of the canvas to the pixel's center
            double xOffset = (px + 0.5) * PixelSize;
            double yOffset = (py + 0.5) * PixelSize;

            //the untransformed coordinates of the pixel in world space.
            //(remember that the camera looks toward -z, so +x is to the *left*.)
            double worldX = HalfWidth - xOffset;
            double worldY = HalfHeight - yOffset;

            //using the camera matrix, transform the canvas point and the origin,
            //and then compute the ray's direction vector.
            //(remember that the canvas is at z=-1)
            var pixel = _transformInverse * Tuple.Point(worldX, worldY, -1);
            var origin = _transformInverse * Tuple.ZeroPoint();
            var direction = (pixel - origin).Normalize();

            var ray = new Ray(origin, direction);
            return ray;

        }

        //public Ray RayForPixelOld(int px, int py)
        //{
        //    //the offset from the edge of the canvas to the pixel's center
        //    double xOffset = (px + 0.5) * PixelSize;
        //    double yOffset = (py + 0.5) * PixelSize;

        //    //the untransformed coordinates of the pixel in world space.
        //    //(remember that the camera looks toward -z, so +x is to the *left*.)
        //    double worldX = HalfWidth - xOffset;
        //    double worldY = HalfHeight - yOffset;

        //    //using the camera matrix, transform the canvas point and the origin,
        //    //and then compute the ray's direction vector.
        //    //(remember that the canvas is at z=-1)
        //    var pixel = Transform.Inverse() * Tuple.Point(worldX, worldY, -1);
        //    var origin = Transform.Inverse() * Tuple.ZeroPoint();
        //    var direction = (pixel - origin).Normalize();

        //    return new Ray(origin, direction);
        //}

        public Canvas Render(World w)
        {
            var c = new Canvas((int)HSize, (int)VSize);
            for (int y = 0; y < VSize; y++)
            {
                for (int x = 0; x < HSize; x++)
                {
                    var ray = RayForPixel(x, y);
                    var color = w.ColorAt(ray);
                    c.SetPixel(x, y, color);
                }
            }
            return c;
        }
    }
}
