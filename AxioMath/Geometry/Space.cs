using AxioMath.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Geometry
{
    public class Space : IGeometricObject
    {
        public Space(int dimension, string name, string[] variableNames = null)
        {
            Name = name;
            Dimension = dimension;
            Points = new Dictionary<string, Point>();
            GeometricShapes = new Dictionary<string, IGeometricShape>();
            GeometricPlaces = new Dictionary<string, IGeometricPlace>();
            VariableNames = variableNames == null ? (new string[] { "x", "y", "z" }).Take(dimension).ToArray() : variableNames;
        }
        public int Dimension { get; }
        public string[] VariableNames { get; }
        public Dictionary<string, Point> Points { get; }
        public Dictionary<string, IGeometricShape> GeometricShapes { get; }
        public Dictionary<string, IGeometricPlace> GeometricPlaces { get; }
        public string Name { get; set; }

        public string GetStandardName(params Point[] vertices)
        {
            return vertices.Select(x => x.Name).Aggregate((a, s) => $"{a}{s}");
        }

        public Point BePoint(string name, params IExpression[] coordinates)
        {
            var point = new Point(this, name, coordinates);
            Points.Add(name, point);
            return point;
        }

        public Rect BeRect(Point firstPoint, Point secondPoint)
        {
            return BeRect(GetStandardName(firstPoint, secondPoint), firstPoint, secondPoint);
        }


        public Rect BeRect(string name, Point firstPoint, Point secondPoint)
        {
            var rect = new Rect(this, name, firstPoint, secondPoint);
            GeometricPlaces.Add(name, rect);
            return rect;
        }

        public Angle BeAngle(Point firstEnd, Point vertix, Point secondEnd)
        {
            return BeAngle(GetStandardName(firstEnd, vertix, secondEnd), firstEnd, vertix, secondEnd);
        }

        public Angle BeAngle(string name, Point firstEnd, Point vertix, Point secondEnd)
        {
            var angle = new Angle(this, name, firstEnd, vertix, secondEnd);
            GeometricShapes.Add(name, angle);
            return angle;
        }

        public GeometricShape BeShape(Point[] vertices)
        {
            return BeShape(GetStandardName(vertices), vertices);
        }
        public GeometricShape BeShape(string name, Point[] vertices)
        {
            var triangle = new GeometricShape(this, name, vertices);
            GeometricShapes.Add(name, triangle);
            return triangle;
        }

    }

}
