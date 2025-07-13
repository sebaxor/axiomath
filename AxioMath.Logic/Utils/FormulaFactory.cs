// Core/Utils/FormulaFactory.cs
using AxioMath.Core.Syntax;
using AxioMath.Logic.Propositional;

namespace AxioMath.Core.Formulas;

public static class FormulaFactory
{
    public static Formula? TryCreateFromNode(FormulaNode node, FormalLanguage language)
    {
        var content = node.ToInfixString();

        try
        {
            return language.BelongsToLanguage(content) ? new Formula(content, node) : null;
        }
        catch
        {
            return null;
        }
    }
    public static Formula CreateDisjunction(Formula left, Formula right, FormalLanguage language)
    {
        var node = new BinaryNode(OperatorSymbols.Or, left.Root, right.Root);
        return new Formula(node.ToInfixString(), node);
    }

    public static Formula CreateConjunction(Formula left, Formula right, FormalLanguage language)
    {
        var node = new BinaryNode(OperatorSymbols.And, left.Root, right.Root);
        return new Formula(node.ToInfixString(), node);
    }

    public static Formula CreateImplication(Formula antecedent, Formula consequent, FormalLanguage language)
    {
        var node = new BinaryNode(OperatorSymbols.Implies, antecedent.Root, consequent.Root);
        return new Formula(node.ToInfixString(), node);
    }

    public static Formula CreateNegation(Formula operand, FormalLanguage language)
    {
        var node = new UnaryNode(OperatorSymbols.Not, operand.Root);
        return new Formula(node.ToInfixString(), node);
    }
}
