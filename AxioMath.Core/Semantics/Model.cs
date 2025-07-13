using AxioMath.Core.Formulas;
using AxioMath.Core.Semantics;

public class Model
{
    public IInterpretation Interpretation { get; }

    public Model(IInterpretation interpretation)
    {
        Interpretation = interpretation;
    }

    public bool Satisfies(Formula formula) => Interpretation.IsTrue(formula);
}

