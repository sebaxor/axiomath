using AxioMath.Core.FormalSystems;
using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;

namespace AxioMath.Logic.DeductionRules.Propositional;

public class ModusTollensRule : IDeductionRule
{
    public IEnumerable<(Formula conclusion, IReadOnlyList<Formula> premises)> Apply(IEnumerable<Formula> premises, FormalLanguage language)
    {
        var list = premises.ToList();

        foreach (var implication in list)
        {
            if (implication.Root is BinaryNode imp && imp.Operator == "→")
            {
                foreach (var negatedQ in list)
                {
                    if (negatedQ.Root is UnaryNode neg &&
                        neg.Operator == "¬" &&
                        AreEquivalent(imp.Right, neg.Operand))
                    {
                        var negatedP = new UnaryNode("¬", imp.Left);
                        var content = Serialize(negatedP);

                        var result = TryCreateFormula(language, content, negatedP);
                        if (result != null)
                            yield return (result, new List<Formula> { implication, negatedQ });
                    }
                }
            }
        }
    }

    private Formula? TryCreateFormula(FormalLanguage language, string content, FormulaNode root)
    {
        try
        {
            if (language.BelongsToLanguage(content))
                return new Formula(content, root);
            else
                Console.WriteLine($"Skipped invalid formula: {content}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating formula from: {content} → {ex.Message}");
        }

        return null;
    }

    private bool AreEquivalent(FormulaNode a, FormulaNode b)
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

    private string Serialize(FormulaNode node)
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

