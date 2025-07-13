using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace AxioMath.Logic.Propositional.DeductionRules;

/// <summary>
/// Regla de adición: de P se infiere P ∨ Q.
/// </summary>
public class AdditionRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        foreach (var premise in premises)
        {
            var qNode = new AtomNode("Q");
            var q = FormulaFactory.TryCreateFromNode(qNode, language);
            if (q is null) continue;

            var disjunction = FormulaFactory.CreateDisjunction(premise, q, language);
            yield return (disjunction, new List<Formula> { premise });
        }
    }
}
