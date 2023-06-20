namespace AxioMath.Geometry
{
    public class GeometricShape : IGeometricShape
    {
        public GeometricShape(Space space, string name, Point[] vertices)
        {
            Name = name;
            Dimension = vertices.First().Dimension;
            Vertices = new List<Point>(vertices);
            Sides = new List<Rect>();
            Angles = new List<Angle>();

            for (int i = 0; i < vertices.Count(); i++)
            {
                var firstPoint = vertices[i];
                var secondPoint = vertices[i + 1 >= vertices.Count() ? i + 1 - vertices.Count() : i + 1];
                var thirdPoint = vertices[i + 2 >= vertices.Count() ? i + 2 - vertices.Count() : i + 2];

                Sides.Add(new Rect(space, $"{firstPoint.Name}{secondPoint.Name}", thirdPoint, secondPoint));
                Angles.Add(new Angle(space, $"{firstPoint.Name}{secondPoint.Name}{thirdPoint.Name}", firstPoint, secondPoint, thirdPoint));
            }

        }
        public Space Space { get; }
        public string Name { get; set; }
        public int Dimension { get; set; }
        public List<Rect> Sides { get; }

        public List<Angle> Angles { get; }

        public List<Point> Vertices { get; }

    }



}
