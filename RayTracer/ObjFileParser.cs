using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RayTracer
{
    public class ObjFileParser
    {
        public List<Tuple> Vertices { get; } = new List<Tuple>();
        public List<Tuple> Normals { get; } = new List<Tuple>();
        public List<Triangle> Triangles { get; } = new List<Triangle>();
        public int Ignored;
        public Group DefaultGroup => Groups[0];
        public List<Group> Groups { get; } = new List<Group>() { new Group() };

        public ObjFileParser(Stream stream)
        {
            Parse(ReadLines(stream).ToArray());
        }

        public ObjFileParser(params string[] str)
        {
            Parse(str);
        }

        private IEnumerable<string> ReadLines(Stream stream)
        {
            using var reader = new StreamReader(stream);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        public static implicit operator Group(ObjFileParser parser)
        {
            var g = new Group();
            g.Add(parser.Groups.ToArray());
            return g;
        }

        public void Parse(params string[] lines)
        {
            var group = DefaultGroup;
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if(parts.Length ==0 )
                {
                    Ignored++;
                    continue;
                }
                switch (parts[0])
                {
                    case "v":
                        ParseVertex(parts);
                        break;
                    case "vn":
                        ParseVertexNormals(parts);
                        break;
                    case "f":
                        if (lines[i].Contains("/"))
                            ParseSmoothTriangles(parts[1..], group);
                        else
                            ParseTriangle(parts[1..], group);
                        break;
                    case "g":
                        group = ParseGroup(parts[1..]);
                        break;
                    default:
                        Ignored++;
                        break;
                }
            }
        }

        private void ParseVertexNormals(string[] parts)
        {
            var x = double.Parse(parts[1]);
            var y = double.Parse(parts[2]);
            var z = double.Parse(parts[3]);
            Normals.Add(Tuple.Vector(x, y, z));
        }

        private Group ParseGroup(string[] vs)
        {
            var group = new Group();
            Groups.Add(group);
            return group;
        }

        private void ParseSmoothTriangles(string[] parts, Group group)
        {
            List<(Tuple v, Tuple n)> vns = new List<(Tuple v, Tuple n)>();
            foreach (string part in parts)
            {
                var viNi = part.Split("/");
                var vi = int.Parse(viNi[0]);
                var ni = int.Parse(viNi[2]);

                vns.Add((Vertices[vi - 1], Normals[ni - 1]));
            }

            var fanedTriangles = FanTriangulation(vns);
            foreach (var triangle in fanedTriangles)
            {
                Triangles.Add(triangle);
                group.Add(triangle);
            }

        }

        private void ParseTriangle(string[] parts, Group group)
        {
            List<Tuple> vertices = new List<Tuple>();
            foreach (string part in parts)
            {
                var index = int.Parse(part);
                vertices.Add(Vertices[index - 1]);
            }

            var fanedTriangles = FanTriangulation(vertices);
            foreach (var triangle in fanedTriangles)
            {
                Triangles.Add(triangle);
                group.Add(triangle);
            }
        }

        private void ParseVertex(string[] parts)
        {
            var x = double.Parse(parts[1]);
            var y = double.Parse(parts[2]);
            var z = double.Parse(parts[3]);
            Vertices.Add(Tuple.Point(x, y, z));
        }

        private List<Triangle> FanTriangulation(List<Tuple> vertices)
        {
            List<Triangle> fanedTriangles = new List<Triangle>();
            for (int i = 1; i <= vertices.Count - 2; i++)
            {
                fanedTriangles.Add(new Triangle(vertices[0], vertices[i], vertices[i + 1]));
            }
            return fanedTriangles;
        }

        private List<SmoothTriangle> FanTriangulation(List<(Tuple v, Tuple n)> vertexNormals)
        {
            List<SmoothTriangle> fanedTriangles = new List<SmoothTriangle>();
            for (int i = 1; i <= vertexNormals.Count - 2; i++)
            {
                fanedTriangles.Add(new SmoothTriangle(
                    vertexNormals[0].v, vertexNormals[i].v, vertexNormals[i + 1].v,
                    vertexNormals[0].n, vertexNormals[i].n, vertexNormals[i + 1].n));
            }
            return fanedTriangles;
        }
    }
}
