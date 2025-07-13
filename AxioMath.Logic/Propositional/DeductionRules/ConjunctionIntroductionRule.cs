using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace AxioMath.Logic.Propositional.DeductionRules;

/// <summary>
/// Implements the ∧ Introduction rule: from A and B, infer (A ∧ B).
/// </summary>
public class ConjunctionIntroductionRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var formulas = premises.ToList();
        var alreadyGenerated = new HashSet<string>(); // content strings

        for (int i = 0; i < formulas.Count; i++)
        {
            for (int j = i + 1; j < formulas.Count; j++)
            {
                var A = formulas[i];
                var B = formulas[j];
                if (A.Root is not AtomNode || B.Root is not AtomNode)
                    continue;


                var content = $"({A.Content} ∧ {B.Content})";

                if (alreadyGenerated.Contains(content))
                    continue;

                var root = new BinaryNode(OperatorSymbols.And, A.Root, B.Root);
                

                var conjunction = new Formula(content, root);

                alreadyGenerated.Add(content);
                yield return (conjunction, new List<Formula> { A, B });
            }
        }
    }
}
