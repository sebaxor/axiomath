namespace AxioMath.Geometry
{
    public class Point : IGeometricObject
    {
        public Point(Space space, string name, params double[] coordinates)
        {
            Space = space;
            Dimension = coordinates.Length;
            Coordinates = coordinates;
            Name = name;
            space.Points.Add(this.Name, this);
        }

        public Space Space { get; }
        public int Dimension { get; }
        public double[] Coordinates { get; }
        public string Name { get; set; }
    }



}
