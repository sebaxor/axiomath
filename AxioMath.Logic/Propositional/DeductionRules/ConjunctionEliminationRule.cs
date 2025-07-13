using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using AxioMath.Logic.Propositional;


namespace AxioMath.Logic.Propositional.DeductionRules;

/// <summary>
/// Implements the ∧ Elimination rule: from A ∧ B, infer A and infer B.
/// </summary>
public class ConjunctionEliminationRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        foreach (var formula in premises)
        {
            if (formula.Root is BinaryNode binary && binary.Operator == OperatorSymbols.And)
            {
                var left = new Formula(NodeToContent(binary.Left), binary.Left);
                var right = new Formula(NodeToContent(binary.Right), binary.Right);

                yield return (left, new List<Formula> { formula });
                yield return (right, new List<Formula> { formula });
            }
        }
    }

    private static string NodeToContent(FormulaNode node)
    {
        return node.ToString() ?? throw new InvalidOperationException("FormulaNode.ToString() returned null");
    }
}
