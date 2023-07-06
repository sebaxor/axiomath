using AxioMath.Core;

namespace AxioMath.Geometry
{
    public class Point : IGeometricObject
    {
        public Point(Space space, string name, params IExpression[] coordinates)
        {
            Space = space;
            Dimension = coordinates.Length;
            Coordinates = coordinates;
            Name = name;
            space.Points.Add(this.Name, this);
        }

        public Space Space { get; }
        public int Dimension { get; }
        public IExpression[] Coordinates { get; }
        public string Name { get; set; }
    }



}
