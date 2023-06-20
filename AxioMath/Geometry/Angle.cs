namespace AxioMath.Geometry
{
    //public class GeometricPlace : IGeometricPlace
    //{
    //    public GeometricPlace(Space space, string name, IExpression condition)
    //    {
    //        Space = space;
    //        Name = name;
    //        Dimension = space.Dimension;
    //        Points = new List<Point>() { firstPoint, secondPoint, thirdPoint };
    //        //TODO Calculate condition
    //        Condition = condition;
    //    }
    //    public Space Space { get; }
    //    public string Name { get; set; }
    //    public int Dimension { get; set; }
    //    public List<Point> Points { get; }
    //    public IExpression Condition { get; set; }
    //}


    public class Angle : IGeometricShape
    {
        public Angle(Space space, string name, Point firstEnd, Point vertix, Point secondEnd)
        {
            Name = name;
            Dimension = firstEnd.Dimension;
            Vertices = new List<Point>() { firstEnd, vertix, secondEnd };
            Sides = new List<Rect>() { new Rect(space,$"{firstEnd.Name}{vertix.Name}", firstEnd, vertix),
                                        new Rect(space,$"{secondEnd.Name}{vertix.Name}", secondEnd, vertix)};
            Angles = new List<Angle>() { this };

        }
        public Space Space { get; }
        public string Name { get; set; }
        public int Dimension { get; set; }
        public List<Rect> Sides { get; }

        public List<Angle> Angles { get; }

        public List<Point> Vertices { get; }
    }



}
