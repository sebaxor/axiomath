// Core/Utils/FormulaFactory.cs
using AxioMath.Core.Syntax;

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
}
