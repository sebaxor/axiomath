using System.Collections.Generic;
using System.Linq;
using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace AxioMath.Logic.DeductionRules.Propositional;

/// <summary>
/// Implements the ∨ Elimination rule: from A ∨ B, A → C, and B → C, infer C.
/// </summary>
public class DisjunctionEliminationRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var formulas = premises.ToList();

        foreach (var disjunction in formulas.Where(f => IsBinaryOperator(f, "∨")))
        {
            var disjNode = (BinaryNode)disjunction.Root;
            var A = disjNode.Left;
            var B = disjNode.Right;

            foreach (var impl1 in formulas.Where(f => IsBinaryOperator(f, "→") && FormulaNodeEquals(((BinaryNode)f.Root).Left, A)))
            {
                var C1 = ((BinaryNode)impl1.Root).Right;

                foreach (var impl2 in formulas.Where(f => IsBinaryOperator(f, "→") && FormulaNodeEquals(((BinaryNode)f.Root).Left, B)))
                {
                    var C2 = ((BinaryNode)impl2.Root).Right;

                    if (FormulaNodeEquals(C1, C2))
                    {
                        var conclusion = new Formula(C1.ToString(), C1); // preserve original content from node
                        yield return (conclusion, new List<Formula> { disjunction, impl1, impl2 });
                    }
                }
            }
        }
    }

    private static bool IsBinaryOperator(Formula formula, string op)
    {
        return formula.Root is BinaryNode binary && binary.Operator == op;
    }

    private static bool FormulaNodeEquals(FormulaNode a, FormulaNode b)
    {
        // Copiado directamente del método privado en tu clase Formula
        if (a is AtomNode atomA && b is AtomNode atomB)
            return atomA.Name == atomB.Name;
        if (a is UnaryNode ua && b is UnaryNode ub)
            return ua.Operator == ub.Operator &&
                   FormulaNodeEquals(ua.Operand, ub.Operand);
        if (a is BinaryNode ba && b is BinaryNode bb)
            return ba.Operator == bb.Operator &&
                   FormulaNodeEquals(ba.Left, bb.Left) &&
                   FormulaNodeEquals(ba.Right, bb.Right);
        return false;
    }
}
