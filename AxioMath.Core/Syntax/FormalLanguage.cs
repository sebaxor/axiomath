using AxioMath.Core.Formulas;
using AxioMath.Core.Parsers;

namespace AxioMath.Core.Syntax;



public class FormalLanguage
{
    public Grammar Grammar { get; }

    public FormalLanguage(Grammar grammar)
    {
        Grammar = grammar;
    }

    public bool BelongsToLanguage(string input) => Grammar.IsValid(input);

    public Formula CreateFormula(string input)
    {
        if (!BelongsToLanguage(input))
            throw new ArgumentException($"Input is not a valid formula in this language. {input}");

        var parser = new GrammarParser(Grammar);
        var node = parser.Parse(input);
        return new Formula(input, node);
    }

}


