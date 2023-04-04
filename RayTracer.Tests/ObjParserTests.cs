using FluentAssertions;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace RayTracer
{
    public class ObjParserTests
    {
        [Fact]
        public void IgnoreUnrecognizedLinesTest()
        {
            var reader = new ObjFileParser("There was a young lady named Bright",
            "who traveled much faster than light.",
                "She set out one day",
                "in a relative way,",
                "and came back the previous night.");

            reader.Ignored.Should().Be(5);
        }

        [Fact]
        public void VertexRecordsTest()
        {
            var reader = new ObjFileParser(
                "v -1 1 0",
            "v -1.0000 0.5000 0.0000",
            "v 1 0 0",
            "v 1 1 0");
            reader.Ignored.Should().Be(0);
            reader.Vertices[0].Should().Be(Tuple.Point(-1, 1, 0));
            reader.Vertices[1].Should().Be(Tuple.Point(-1, 0.5, 0));
            reader.Vertices[2].Should().Be(Tuple.Point(1, 0, 0));
            reader.Vertices[3].Should().Be(Tuple.Point(1, 1, 0));
        }

        [Fact]
        public void ParsingTriangleFacesTest()
        {
            var reader = new ObjFileParser(
                "v -1 1 0",
                "v -1 0 0",
                "v 1 0 0",
                "v 1 1 0",
                "f 1 2 3",
                "f 1 3 4");

            reader.Triangles[0].P1.Should().Be(reader.Vertices[0]);
            reader.Triangles[0].P2.Should().Be(reader.Vertices[1]);
            reader.Triangles[0].P3.Should().Be(reader.Vertices[2]);
            reader.Triangles[1].P1.Should().Be(reader.Vertices[0]);
            reader.Triangles[1].P2.Should().Be(reader.Vertices[2]);
            reader.Triangles[1].P3.Should().Be(reader.Vertices[3]);
        }

        [Fact]
        public void TriangulatingPolygons()
        {
            var reader = new ObjFileParser(
                "v -1 1 0",
                "v -1 0 0",
                "v 1 0 0",
                "v 1 1 0",
                "v 0 2 0",
                "f 1 2 3 4 5");

            Triangle t1 = (Triangle)reader.DefaultGroup[0];
            Triangle t2 = (Triangle)reader.DefaultGroup[1];
            Triangle t3 = (Triangle)reader.DefaultGroup[2];

            t1.P1.Should().Be(reader.Vertices[0]);
            t1.P2.Should().Be(reader.Vertices[1]);
            t1.P3.Should().Be(reader.Vertices[2]);
            t2.P1.Should().Be(reader.Vertices[0]);
            t2.P2.Should().Be(reader.Vertices[2]);
            t2.P3.Should().Be(reader.Vertices[3]);
            t3.P1.Should().Be(reader.Vertices[0]);
            t3.P2.Should().Be(reader.Vertices[3]);
            t3.P3.Should().Be(reader.Vertices[4]);
        }

        [Fact]
        public void TrianglesInGroupTest()
        {
            var reader = new ObjFileParser(
                "v -1 1 0",
            "v -1 0 0",
            "v 1 0 0",
            "v 1 1 0",
            "g FirstGroup",
            "f 1 2 3",
            "g SecondGroup",
            "f 1 3 4");
            var t1 = reader.Groups[1][0] as Triangle;
            var t2 = reader.Groups[2][0] as Triangle;

            t1.P1.Should().Be(reader.Vertices[0]);
            t1.P2.Should().Be(reader.Vertices[1]);
            t1.P3.Should().Be(reader.Vertices[2]);
            t2.P1.Should().Be(reader.Vertices[0]);
            t2.P2.Should().Be(reader.Vertices[2]);
            t2.P3.Should().Be(reader.Vertices[3]);
        }

        [Fact]
        public void ObjectFileToGroup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "RayTracer.triangles.obj";

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            Group parsedGroup = new ObjFileParser(stream);
            parsedGroup.Shapes.Count.Should().Be(3);
        }

        [Fact]
        public void VertexNormalRecordTest()
        {
            var reader = new ObjFileParser(
                "vn 0 0 1",
            "vn 0.707 0 -0.707",
            "vn 1 2 3");

           reader.Normals[0].Should().Be(Tuple.Vector(0, 0, 1));
           reader.Normals[1].Should().Be(Tuple.Vector(0.707, 0, -0.707));
           reader.Normals[2].Should().Be(Tuple.Vector(1, 2, 3));
        }

        [Fact]
        public void TrianglesWithNormalsTest()
        {
            var reader = new ObjFileParser(
                "v 0 1 0",
                "v -1 0 0",
                "v 1 0 0",
                "",
                "vn -1 0 0",
                "vn 1 0 0",
                "vn 0 1 0",
                "f 1//3 2//1 3//2",
                "f 1/0/3 2/102/1 3/14/2");
            var t1 = reader.DefaultGroup[0] as SmoothTriangle;
            var t2 = reader.DefaultGroup[0] as SmoothTriangle;

            t1.P1.Should().Be(reader.Vertices[0]);
            t1.P2.Should().Be(reader.Vertices[1]);
            t1.P3.Should().Be(reader.Vertices[2]);
            t1.N1.Should().Be(reader.Normals[2]);
            t1.N2.Should().Be(reader.Normals[0]);
            t1.N3.Should().Be(reader.Normals[1]);

            t2.P1.Should().Be(reader.Vertices[0]);
            t2.P2.Should().Be(reader.Vertices[1]);
            t2.P3.Should().Be(reader.Vertices[2]);
            t2.N1.Should().Be(reader.Normals[2]);
            t2.N2.Should().Be(reader.Normals[0]);
            t2.N3.Should().Be(reader.Normals[1]);
        }
    }
}
