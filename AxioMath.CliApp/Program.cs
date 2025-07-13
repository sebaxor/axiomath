using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== AXIOMATH ===\n");

        // Construcción del lenguaje con símbolos básicos
        var axiomStrings = new[]
{
    "p",                // simple proposición (usada por Modus Ponens y conjunción)
    "q",                // otra proposición base
    "(p → q)",          // Modus Ponens
    "(q → r)",          // cadena de implicación
    "(p ∨ s)",          // disyunción
    "(s → t)",          // complemento para disyunción
    "¬t",               // premisa para Modus Tollens
    "(q ∧ r)",          // premisa para eliminación de conjunción
    "(¬r → ¬p)",         // para explorar contrarrecíproca con Modus Tollens
        // Axiomas nuevos para Disjunction Elimination
    "(a ∨ b)",
    "(a → z)",
    "(b → z)"
};


        // Extrae todos los átomos usados (letras minúsculas que no son operadores)
        var atomNames = axiomStrings
            .SelectMany(s => s)
            .Where(char.IsLetter)
            .Select(c => c.ToString())
            .Distinct();

        var language = PropositionalLanguageBuilder.Build(atomNames);
        var axioms = axiomStrings.Select(language.CreateFormula).ToArray();



        var rules = new IDeductionRule[]
        {
            new ModusPonensRule(),
            new ModusTollensRule(),
            new DisjunctionEliminationRule(),
            new ConjunctionIntroductionRule(),
            new ConjunctionEliminationRule()
        };

        var system = new FormalSystem(language, axioms, rules);
        var theory = new FormalTheory(system);

        Console.WriteLine("[Axiomas]");
        foreach (var ax in axioms)
            Console.WriteLine($"  • {ax}");

        Console.WriteLine("\n[Teoremas Derivados]");
        foreach (var th in theory.Theorems.Where(t => t.Rule != null))
        {
            Console.WriteLine(th.ToString());
            Console.WriteLine(new string('-', 40));
        }



        Console.WriteLine("\n[Resumen]");
        Console.WriteLine($"  Total de teoremas: {theory.Theorems.Count()}");

    }
}
