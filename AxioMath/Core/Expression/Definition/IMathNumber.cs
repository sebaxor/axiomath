namespace AxioMath.Core.Expression.Definition
{

    public interface IMathNumber
    {
        int SetLevel { get; }
        double RealPart { get; }
        double? ImaginaryPart { get; }
    }


}
