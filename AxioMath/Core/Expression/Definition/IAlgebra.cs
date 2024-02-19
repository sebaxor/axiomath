namespace AxioMath.Core.Expression.Definition
{
    // Definición de la interfaz Algebra
    public interface IAlgebra<T>
    {
        T Number(T value);
        T Add(T left, T right);
        T Multiply(T left, T right);
    }
}


