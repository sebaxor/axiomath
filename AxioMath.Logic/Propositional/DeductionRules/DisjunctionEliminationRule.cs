using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace AxioMath.Logic.Propositional.DeductionRules;

public class DisjunctionEliminationRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var list = premises.ToList();

        foreach (var disjunction in list.Where(f => f.IsDisjunction()))
        {
            var left = disjunction.DisjunctionLeft();
            var right = disjunction.DisjunctionRight();

            if (left == null || right == null)
                continue;

            foreach (var impLeft in list.Where(f => f.IsImplication()))
            {
                var antLeft = impLeft.ImplicationAntecedent();
                var conLeft = impLeft.ImplicationConsequent();

                if (antLeft is null || conLeft is null || !left.StructurallyEquals(antLeft))
                    continue;

                foreach (var impRight in list.Where(f => f.IsImplication()))
                {
                    var antRight = impRight.ImplicationAntecedent();
                    var conRight = impRight.ImplicationConsequent();

                    if (antRight is null || conRight is null || !right.StructurallyEquals(antRight))
                        continue;

                    if (conLeft.StructurallyEquals(conRight))
                    {
                        var conclusion = FormulaFactory.TryCreateFromNode(conLeft, language);
                        if (conclusion is not null)
                        {
                            yield return (conclusion, new[] { disjunction, impLeft, impRight });
                        }
                    }
                }
            }
        }
    }
}
