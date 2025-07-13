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
        var formulas = premises.ToList();

        for (int i = 0; i < formulas.Count; i++)
        {
            for (int j = 0; j < formulas.Count; j++)
            {
                if (i == j) continue;

                var f1 = formulas[i];
                var f2 = formulas[j];

                if (f1.Root is not BinaryNode imp1 || imp1.Operator != OperatorSymbols.Implies) continue;
                if (f2.Root is not BinaryNode imp2 || imp2.Operator != OperatorSymbols.Implies) continue;

                if (AreEquivalent(imp1.Right, imp2.Left))
                {
                    var newRoot = new BinaryNode(OperatorSymbols.Implies, imp1.Left, imp2.Right);
                    var content = $"({Serialize(imp1.Left)} {OperatorSymbols.Implies} {Serialize(imp2.Right)})";

                    if (language.BelongsToLanguage(content))
                    {
                        var result = new Formula(content, newRoot);
                        yield return (result, new List<Formula> { f1, f2 });
                    }
                }
            }
        }
    }

    private static bool AreEquivalent(FormulaNode a, FormulaNode b)
    {
        if (a is AtomNode atomA && b is AtomNode atomB)
            return atomA.Name == atomB.Name;
        if (a is UnaryNode una && b is UnaryNode unb)
            return una.Operator == unb.Operator && AreEquivalent(una.Operand, unb.Operand);
        if (a is BinaryNode bina && b is BinaryNode binb)
            return bina.Operator == binb.Operator &&
                   AreEquivalent(bina.Left, binb.Left) &&
                   AreEquivalent(bina.Right, binb.Right);
        return false;
    }

    private static string Serialize(FormulaNode node)
    {
        return node switch
        {
            AtomNode a => a.Name,
            UnaryNode u => $"{u.Operator}{Serialize(u.Operand)}",
            BinaryNode b => $"({Serialize(b.Left)} {b.Operator} {Serialize(b.Right)})",
            _ => throw new NotSupportedException()
        };
    }
}
