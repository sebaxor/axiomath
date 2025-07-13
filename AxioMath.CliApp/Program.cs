using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using AxioMath.Logic.DeductionRules.Propositional;

// Definición de símbolos terminales
var not = new Symbol("¬", true);
var and = new Symbol("∧", true);
var or = new Symbol("∨", true);
var implies = new Symbol("→", true);
var iff = new Symbol("↔", true);
var lparen = new Symbol("(", true);
var rparen = new Symbol(")", true);

var p = new Symbol("p", true);
var q = new Symbol("q", true);
var r = new Symbol("r", true);

// Definición de símbolos no terminales
var atom = new Symbol("Atom", false);
var formula = new Symbol("Formula", false);

var grammar = new Grammar(formula);

// Gramática
grammar.AddRule(atom, new[] { p }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { q }, RuleInterpretation.Atom);
grammar.AddRule(atom, new[] { r }, RuleInterpretation.Atom);

grammar.AddRule(formula, new[] { atom }, RuleInterpretation.Atom);
grammar.AddRule(formula, new[] { not, formula }, RuleInterpretation.Unary, "¬");
grammar.AddRule(formula, new[] { lparen, formula, and, formula, rparen }, RuleInterpretation.Binary, "∧");
grammar.AddRule(formula, new[] { lparen, formula, or, formula, rparen }, RuleInterpretation.Binary, "∨");
grammar.AddRule(formula, new[] { lparen, formula, implies, formula, rparen }, RuleInterpretation.Binary, "→");
grammar.AddRule(formula, new[] { lparen, formula, iff, formula, rparen }, RuleInterpretation.Binary, "↔");

// Lenguaje
var language = new FormalLanguage(grammar);

// Axiomas que permiten probar Modus Ponens y Modus Tollens
var f1 = language.CreateFormula("(p → q)");
var f2 = language.CreateFormula("p");      // Para activar Modus Ponens
var f3 = language.CreateFormula("¬q");     // Para activar Modus Tollens

var axioms = new[] { f1, f2, f3 };

// Reglas disponibles
var rules = new IDeductionRule[]
{
    new ModusPonensRule(),
    new ModusTollensRule()
};

// Sistema y teoría
var system = new FormalSystem(language, axioms, rules);
var theory = new FormalTheory(system);

// Mostrar teoremas deducidos
foreach (var t in theory.Theorems)
    Console.WriteLine("Teorema: " + t.Formula);
