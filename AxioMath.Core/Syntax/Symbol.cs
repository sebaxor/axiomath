namespace AxioMath.Core.Syntax;
public class Symbol
{
    public string Value { get; }
    public bool IsTerminal { get; }

    public Symbol(string value, bool isTerminal)
    {
        Value = value;
        IsTerminal = isTerminal;
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is Symbol s && s.Value == Value && s.IsTerminal == IsTerminal;
    public override int GetHashCode() => HashCode.Combine(Value, IsTerminal);
}