using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Demo.Scenes
{
    public class TableScene : AbstractScene
    {
        private IShape GetLeg(double tx, double ty, double tz)
        {
            return new Cube
            {
                Material = new Material(Color._Brown,
                specular: 0.4, shininess: 5),
                Transform = Matrix.Transformation()
                .Translation(tx, ty, tz)
                .Scaling(0.05, 1, 0.05)
                .Apply()
            };
        }
        public override void Initialize()
        {
            IShape floor = new Plane();
            floor.Material = new Material(Color._LightBrown);

            var legOne = GetLeg(3, 0.3, 3);
            var legTwo = GetLeg(25, 0.3, 3);
            var legThree = GetLeg(25, 0.3, 26);
            var legFour = GetLeg(3, 0.3, 26);

            var table = new Cube
            {
                Material = new Material(Color._Brown,
                specular: 0.4, shininess: 5),
                Transform = Matrix.Transformation()
                .Translation(1, 27, 1)
                .Scaling(0.7, 0.05, .7)
                .Apply()
            };

            var cube = new Cube
            {
                Material = new Material(Color._Green, specular: 0.9, shininess: 300, reflective: 0.9, transparency: 0.9, refractiveIndex: 1.5),
                Transform = Matrix.Transformation()
                .Translation(10, 29, 10)
                .Scaling(0.05, 0.05, 0.05)
                .Apply()
            };

            Add(new[] { floor, legOne, legTwo, legThree, legFour, table, cube });

            Light(-10, 10, -10);
            //Light(10, 10, -10);
            SetCameraSettings(new CameraSettings
            {
                //ViewFrom = Tuple.Point(-1.2, 2, -3),
                ViewFrom = Tuple.Point(-2.2, 3, -3),
                //ViewFrom = Tuple.Point(2.2, 2, -3),
                //ViewFrom = Tuple.Point(5.2, 2, 2),
                ViewTo = Tuple.Vector(1, 1, 0)
            });
        }
    }
}
