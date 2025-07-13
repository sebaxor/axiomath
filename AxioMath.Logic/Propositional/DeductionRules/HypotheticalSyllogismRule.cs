using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace AxioMath.Logic.Propositional.DeductionRules;

/// <summary>
/// Hypothetical Syllogism: from (p → q) and (q → r), infer (p → r)
/// </summary>
public class HypotheticalSyllogismRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var list = premises.Where(f => f.IsImplication()).ToList();

        foreach (var first in list)
        {
            var firstAnte = first.ImplicationAntecedent();
            var firstCons = first.ImplicationConsequent();

            foreach (var second in list)
            {
                var secondAnte = second.ImplicationAntecedent();
                var secondCons = second.ImplicationConsequent();

                if (firstCons is not null && secondAnte is not null &&
                    firstCons.StructurallyEquals(secondAnte) &&
                    firstAnte is not null && secondCons is not null)
                {
                    var result = FormulaFactory.TryCreateFromNode(
                        new BinaryNode("→", firstAnte, secondCons), language);

                    if (result != null)
                        yield return (result, new List<Formula> { first, second });
                }
            }
        }
    }
}
