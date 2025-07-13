using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace AxioMath.Logic.Propositional.DeductionRules;

public class ModusTollensRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var list = premises.ToList();

        foreach (var implication in list.Where(f => f.IsImplication()))
        {
            var ant = implication.ImplicationAntecedent();
            var con = implication.ImplicationConsequent();

            if (ant is null || con is null)
                continue;

            var notQ = FormulaFactory.TryCreateFromNode(new UnaryNode("¬", con), language);
            if (notQ == null || !list.Any(p => p.Root.StructurallyEquals(notQ.Root)))
                continue;

            var notPNode = new UnaryNode("¬", ant);
            var notP = FormulaFactory.TryCreateFromNode(notPNode, language);

            if (notP is not null)
            {
                var premiseNotQ = list.First(p => p.Root.StructurallyEquals(notQ.Root));
                yield return (notP, new[] { implication, premiseNotQ });
            }
        }
    }
}
