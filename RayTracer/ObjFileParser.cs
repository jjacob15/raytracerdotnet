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
                switch (parts[0])
                {
                    case "v":
                        ParseVertex(parts);
                        break;
                    case "f":
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

        private Group ParseGroup(string[] vs)
        {
            var group = new Group();
            Groups.Add(group);
            return group;
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
    }
}
