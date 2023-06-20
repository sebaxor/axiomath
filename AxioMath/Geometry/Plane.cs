using AxioMath.Core;

namespace AxioMath.Geometry
{
    public class Plane : IGeometricPlace
    {
        public Plane(Space space, string name, Point firstPoint, Point secondPoint, Point thirdPoint)
        {
            Space = space;
            Name = name;
            Dimension = firstPoint.Dimension;
            Points = new List<Point>() { firstPoint, secondPoint, thirdPoint };
            //TODO Calculate condition
            Condition = null;
        }
        public Space Space { get; }
        public string Name { get; set; }
        public int Dimension { get; set; }
        public List<Point> Points { get; }
        public IExpression Condition { get; set; }
    }



}
