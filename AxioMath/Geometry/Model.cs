using AxioMath.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AxioMath.Geometry
{
    public interface IGeometricObject
    {

        int Dimension { get; }
        string Name { get; set; }
    }

    public interface IGeometricPlace : IGeometricObject
    {
        List<Point> Points { get; }
        IExpression Condition { get; }

    }

    public interface IGeometricShape : IGeometricObject
    {
        List<Rect> Sides { get; }
        List<Angle> Angles { get; }
        List<Point> Vertices { get; }

    }



}
