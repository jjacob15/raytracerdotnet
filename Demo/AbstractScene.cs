﻿using RayTracer;
using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Demo
{
    public abstract class AbstractScene
    {
        public IWorld World { get; }

        public AbstractScene()
        {
            World = new World();
            World.SetLight(new PointLight(Tuple.Point(-10, 10, -10), Color.White));
        }

        public abstract void Initialize();

        public void Add<T>(T shape) where T : IShape
        {
            World.AddShape(shape);
        }

        public void Add(params IShape[] shapes)
        {
            foreach (var shape in shapes) Add<IShape>(shape);
        }

        public void Add(ILight light)
        {
            World.SetLight(light);
        }
    }
}