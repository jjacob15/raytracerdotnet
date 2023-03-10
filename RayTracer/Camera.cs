using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Camera
    {
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
        public Matrix Transform { get; set; } = Matrix.Identity();

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
            var pixel = Transform.Inverse() * Tuple.Point(worldX, worldY, -1);
            var origin = Transform.Inverse() * Tuple.ZeroPoint();
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(World w)
        {
            var c = new Canvas((int)HSize, (int)VSize);
            for (int y = 0; y < VSize; y++)
            {
                for (int x = 0; x < VSize; x++)
                {
                    var ray = RayForPixel(x, y);
                    var color = w.ColorAt(ray);
                    c[x, y] = color;
                }
            }
            return c;
        }
    }
}
