﻿using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scenes
{
    public class SimpleSceneTwo : AbstractScene
    {
        public override void Initialize()
        {
            var floor = new Plane();
            floor.Material = new Material(new Color(1, 0.9, 0.9));
            floor.Material.Pattern = new CheckerPattern();
            floor.Material.Specular = 0;
            Add(floor);

            var middle = new Sphere();
            middle.Transform = Matrix.Transformation().Translation(-0.5, 1, 0.5).Apply();
            middle.Material = new Material(new Color(0.1, 1, 0.5));
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;
            Add(middle);

            var right = new Sphere();
            right.Transform = Matrix.Transformation().Scaling(0.5, 0.5, 0.5).Translation(1.5, 0.5, -0.5).Apply();
            right.Material = new Material(new Color(0.5, 1, 0.1));
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            Add(right);


            var left = new Sphere();
            left.Transform = Matrix.Transformation().Scaling(.33, .33, .33).Translation(-1.5, 0.33, -0.75).Apply();
            left.Material = new Material(new Color(1, 0.8, 0.1));
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            Add(left);
        }
    }
}
