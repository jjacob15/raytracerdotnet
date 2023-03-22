using FluentAssertions;
using RayTracer.Lights;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RayTracer
{
    public class IntersectionTests
    {
        [Fact]
        public void IntersectionEncapsulateT()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = new Intersections();
            s.Intersect(ray, xs);

            Assert.Equal(2, xs.Count);
            Assert.Equal(s, xs[0].Object);
            Assert.Equal(s, xs[1].Object);
        }

        [Fact]
        public void HitWithPositiveT()
        {
            var s = new Sphere();
            var i1 = s.Intersection(1);
            var i2 = s.Intersection(2);

            var xs = new Intersections(i1, i2);
            var hit = xs.Hit();

            Assert.Equal(hit, i1);
        }

        [Fact]
        public void HitWithNegT()
        {
            var s = new Sphere();
            var i1 = s.Intersection(-1);
            var i2 = s.Intersection(1);

            var xs = new Intersections(i1, i2);
            var hit = xs.Hit();

            Assert.Equal(hit, i2);
        }

        [Fact]
        public void HitLowesthNegT()
        {
            var s = new Sphere();
            var i1 = s.Intersection(5);
            var i2 = s.Intersection(7);
            var i3 = s.Intersection(-3);
            var i4 = s.Intersection(2);

            var xs = new Intersections(i1, i2, i3, i4);
            var hit = xs.Hit();

            Assert.Equal(hit, i4);
        }

        [Fact]
        public void IntersectingScaledSphereWithARay()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            s.Transform = Matrix.Transformation().Scaling(2, 2, 2).Apply();
            var xs = new Intersections();
            s.Intersect(ray, xs);

            Assert.Equal(2, xs.Count);
            Assert.True(xs[0].T.DoubleEqual(3));
            Assert.True(xs[1].T.DoubleEqual(7));
        }


        [Fact]
        public void IntersectingTranslatedSphereWithARay()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            s.Transform = Matrix.Transformation().Translation(5, 0, 0).Apply();
            var xs = new Intersections();
            s.Intersect(ray, xs);

            Assert.Equal(0, xs.Count);
        }

        [Fact]
        public void PrecomputeStateOfAnIntersection()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 4);
            var comps = i.PrepareComputations(r);

            comps.T.Should().Be(i.T);
            comps.Object.Should().Be(i.Object);
            comps.Point.Should().Be(Tuple.Point(0, 0, -1));
            comps.EyeV.Should().Be(Tuple.Vector(0, 0, -1));
            comps.NormalV.Should().Be(Tuple.Vector(0, 0, -1));
        }

        [Fact]
        public void HitWhenIntersectionOccursOutside()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 4);
            var comp = i.PrepareComputations(r);

            comp.Inside.Should().Be(false);
        }

        [Fact]
        public void HitWhenIntersectionOccursInside()
        {
            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            var i = new Intersection(s, 1);

            var comp = i.PrepareComputations(r);
            comp.Point.Should().Be(Tuple.Point(0, 0, 1));
            comp.EyeV.Should().Be(Tuple.Vector(0, 0, -1));
            comp.Inside.Should().Be(true);
            comp.NormalV.Should().Be(Tuple.Vector(0, 0, -1));
        }

        [Fact]
        public void ShadingAnIntersection()
        {
            var w = ObjectFactory.DefaultWorld();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var shape = w.Shapes.First();
            var i = new Intersection(shape, 4);
            var comps = i.PrepareComputations(r);
            w.ShadeHits(comps).Should().Be(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void ShadingAnIntersectionFromInside()
        {
            var w = ObjectFactory.DefaultWorld();
            w.SetLight(new PointLight(Tuple.Point(0, 0.25, 0), new Color(1, 1, 1)));

            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var shape = w.Shapes[1];
            var i = new Intersection(shape, 0.5);
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHits(comps);
            c.Red.Should().BeApproximately(0.90498, 1e-4);
            c.Green.Should().BeApproximately(0.90498, 1e-4);
            c.Blue.Should().BeApproximately(0.90498, 1e-4);
        }

        [Fact]
        public void ColorWhenRayMisses()
        {
            var w = ObjectFactory.DefaultWorld();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 1, 0));
            w.ColorAt(r).Should().Be(Color.Black);
        }

        [Fact]
        public void ColorWhenRayHits()
        {
            var w = ObjectFactory.DefaultWorld();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            w.ColorAt(r).Should().Be(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void ColorWhenIntersectionBehindRay()
        {
            var w = ObjectFactory.DefaultWorld();
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            w.ColorAt(r).Should().Be(new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void ColorWithIntersectionBehindTheRay()
        {
            var w = ObjectFactory.DefaultWorld();
            var outer = w.Shapes.First();
            outer.Material.Ambient = 1;

            var inner = w.Shapes[1];
            inner.Material.Ambient = 1;
            var r = new Ray(Tuple.Point(0, 0, .75), Tuple.Vector(0, 0, -1));
            w.ColorAt(r).Should().Be(inner.Material.Color);
        }

        [Fact]
        public void HitShouldOffsetThePoint()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = new Sphere();
            s.Transform = Matrix.Transformation().Translation(0, 0, 1).Apply();
            var i = s.Intersection(5);
            var comps = i.PrepareComputations(r);
            comps.OverPoint.Z.Should().BeLessThan(-Constants.Epsilon / 2);
            comps.Point.Z.Should().BeGreaterThan(comps.OverPoint.Z);
        }

        [Fact]
        public void ReflectNormalTest()
        {
            var p = new Plane();
            var r = new Ray(Tuple.Point(0, 1, -1), Tuple.Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(p, Math.Sqrt(2));
            var comps = i.PrepareComputations(r);
            comps.ReflectV.Should().Be(Tuple.Vector(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
        }

        [Fact]
        public void ReflectColorForNonreflectiveSurface()
        {
            var w = ObjectFactory.DefaultWorld();
            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var s = w.Shapes[1];
            s.Material.Ambient = 1;
            var i = new Intersection(s, 1);
            var comps = i.PrepareComputations(r);
            w.ReflectedColor(comps).Should().Be(Color.Black);
        }

        [Fact]
        public void ReflectColorForReflectiveSurface()
        {
            var w = ObjectFactory.DefaultWorld();
            var p = new Plane();
            p.Material.Reflective = 0.5;
            p.Transform = Matrix.Transformation().Translation(0, -1, 0).Apply();
            w.AddShape(p);

            var r = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(p, Math.Sqrt(2));
            var comps = i.PrepareComputations(r);
            var c = w.ReflectedColor(comps);
            c.Red.Should().BeApproximately(0.19032, 1e-4);
            c.Green.Should().BeApproximately(0.2379, 1e-4);
            c.Blue.Should().BeApproximately(0.14274, 1e-4);
        }
        [Fact]
        public void ShadeHitWithReflectiveMaterial()
        {
            var w = ObjectFactory.DefaultWorld();
            var p = new Plane();
            p.Material.Reflective = 0.5;
            p.Transform = Matrix.Transformation().Translation(0, -1, 0).Apply();
            w.AddShape(p);

            var r = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(p, Math.Sqrt(2));
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHits(comps);
            c.Red.Should().BeApproximately(0.87677, 1e-4);
            c.Green.Should().BeApproximately(0.92436, 1e-4);
            c.Blue.Should().BeApproximately(0.82918, 1e-4);
            //c.Should().Be(new Color(0.87677, 0.92436, 0.82918));
        }

        [Fact]
        public void ColorAtMutuallyReflectiveSurfaces()
        {
            var w = new World();
            w.SetLight(new PointLight(Tuple.Point(0, 0, 0), Color.White));
            var lower = new Plane();
            lower.Material.Reflective = 1;
            lower.Transform = Matrix.Transformation().Translation(0, -1, 0).Apply();
            w.AddShape(lower);

            var upper = new Plane();
            upper.Material.Reflective = 1;
            upper.Transform = Matrix.Transformation().Translation(0, 1, 0).Apply();
            w.AddShape(upper);

            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 1, 0));
            var color = w.ColorAt(r);
            color.Should().NotBeNull();
        }

        [Fact]
        public void ReflectedColorAtMaxDepth()
        {
            var w = ObjectFactory.DefaultWorld();
            var shape = new Plane();
            shape.Material.Reflective = 0.5;
            shape.Transform = Matrix.Transformation().Translation(0, -1, 0).Apply();
            w.AddShape(shape);

            var r = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(shape, Math.Sqrt(2));
            var comps = i.PrepareComputations(r);
            w.ReflectedColor(comps, 0).Should().Be(Color.Black);
        }

        [Fact]
        public void RefractiveIndexOfDefaultMaterial()
        {
            var s = Sphere.GlassSphere();
            s.Material.Transparency.Should().Be(1);
            s.Material.RefractiveIndex.Should().Be(1.5);
        }

        [Theory]
        [InlineData(0, 1, 1.5)]
        [InlineData(1, 1.5, 2)]
        [InlineData(2, 2, 2.5)]
        [InlineData(3, 2.5, 2.5)]
        [InlineData(4, 2.5, 1.5)]
        [InlineData(5, 1.5, 1)]
        public void FindingN1AndN2AtVariousIntersections(int i, double n1, double n2)
        {
            var a = Sphere.GlassSphere();
            a.Transform = Matrix.Transformation().Scaling(2, 2, 2).Apply();
            a.Material.RefractiveIndex = 1.5;

            var b = Sphere.GlassSphere();
            b.Transform = Matrix.Transformation().Translation(0, 0, -0.25).Apply();
            b.Material.RefractiveIndex = 2;

            var c = Sphere.GlassSphere();
            b.Transform = Matrix.Transformation().Translation(0, 0, 0.25).Apply();
            c.Material.RefractiveIndex = 2.5;

            var r = new Ray(Tuple.Point(0, 0, -4), Tuple.Vector(0, 0, 1));
            var xs = new Intersections(new Intersection(a, 2), new Intersection(b, 2.75), new Intersection(c, 3.25), new Intersection(b, 4.75), new Intersection(c, 5.25), new Intersection(a, 6));

            var comps = xs[i].PrepareComputations(r, xs);
            comps.N1.Should().Be(n1);
            comps.N2.Should().Be(n2);
        }

        [Fact]
        public void UnderpointIsOffsetBelowSurface()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var s = Sphere.GlassSphere();
            s.Transform = Matrix.Transformation().Translation(0, 0, 1).Apply();
            var i = new Intersection(s, 5);
            var xs = new Intersections(i);
            var comps = i.PrepareComputations(r, xs);
            comps.UnderPoint.Z.Should().BeGreaterThan(Constants.Epsilon / 2);
            comps.Point.Z.Should().BeLessThan(comps.UnderPoint.Z);
        }

        [Fact]
        public void SchlickApproxUnderTotalInternalReflection()
        {
            var sqrtTwo = Math.Sqrt(2);
            var s = Sphere.GlassSphere();
            var r = new Ray(Tuple.Point(0, 0, sqrtTwo / 2), Tuple.Vector(0, 1, 0));
            var xs = new Intersections(new Intersection(s, -sqrtTwo / 2), new Intersection(s, sqrtTwo / 2));
            var comps = xs[1].PrepareComputations(r, xs);
            comps.Schlick().Should().Be(1.0);
        }


        [Fact]
        public void SchlickApproxWithPerpendicularViewingAngle()
        {
            var s = Sphere.GlassSphere();
            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 1, 0));
            var xs = new Intersections(new Intersection(s, -1), new Intersection(s, 1));
            var comps = xs[1].PrepareComputations(r, xs);
            comps.Schlick().Should().BeApproximately(0.04, Constants.Epsilon);
        }

        [Fact]
        public void SchlickApproxWithSmallAngleN2GTN1()
        {
            var s = Sphere.GlassSphere();
            var r = new Ray(Tuple.Point(0, 0.99, -2), Tuple.Vector(0, 0, 1));
            var xs = new Intersections(new Intersection(s, 1.8589));
            var comps = xs[0].PrepareComputations(r, xs);
            comps.Schlick().Should().BeApproximately(0.48873, Constants.Epsilon);
        }
    }
}
