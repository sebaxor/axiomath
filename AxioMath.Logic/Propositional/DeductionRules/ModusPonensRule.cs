
using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;


namespace AxioMath.Logic.Propositional.DeductionRules;

public class ModusPonensRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var list = premises.ToList();
        var seen = new HashSet<string>(); // evita duplicados por contenido

        foreach (var p in list)
        {
            foreach (var implication in list.Where(f => f.IsImplication()))
            {
                var antecedent = implication.ImplicationAntecedent();
                if (antecedent != null && p.Root.StructurallyEquals(antecedent))
                {
                    var conclusionNode = implication.ImplicationConsequent();
                    if (conclusionNode is not null)
                    {
                        var conclusion = FormulaFactory.TryCreateFromNode(conclusionNode, language);
                        if (conclusion != null && seen.Add(conclusion.Content))
                        {
                            yield return (conclusion, new List<Formula> { p, implication });
                        }
                    }
                }
            }
        }
    }

}
