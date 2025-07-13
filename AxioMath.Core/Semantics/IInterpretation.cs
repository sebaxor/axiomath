using AxioMath.Core.Formulas;

namespace AxioMath.Core.Semantics;
public interface IInterpretation
{
    /// <summary>Evalúa si una fórmula es verdadera bajo esta interpretación.</summary>
    bool IsTrue(Formula formula);
}