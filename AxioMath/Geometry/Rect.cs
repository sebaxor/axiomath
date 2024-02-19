using AxioMath.Core.Expression.Definition;

namespace AxioMath.Geometry
{
    public class Rect : IGeometricPlace
    {
        public Rect(Space space, string name, Point firstPoint, Point secondPoint)
        {
            Space = space;
            Name = name;
            Dimension = firstPoint.Dimension;
            Points = new List<Point>() { firstPoint, secondPoint };
            //TODO Calculate condition
            Condition = null;
            space.GeometricPlaces.Add(this.Name, this);
        }
        public Space Space { get; }
        public string Name { get; set; }
        public int Dimension { get; set; }
        public List<Point> Points { get; }
        public IExpression Condition { get; set; }
    }



}
