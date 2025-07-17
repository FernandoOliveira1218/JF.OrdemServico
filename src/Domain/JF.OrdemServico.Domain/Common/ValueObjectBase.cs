using System.Reflection;

namespace JF.OrdemServico.Domain.Common;

public abstract class ValueObjectBase<T> where T : ValueObjectBase<T>
{
    public string Value { get; private set; }

    public string Name { get; private set; }

    protected ValueObjectBase(string value, string name)
    {
        Value = value;
        Name = name;
    }

    public override string ToString() => Name;

    public static implicit operator string(ValueObjectBase<T> value) => value.Value;

    public static IEnumerable<T> GetAll()
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public static T FromValue(string value)
    {
        return GetAll().FirstOrDefault(x => x.Value.Equals(value)) ?? throw new ArgumentException($"Valor inválido para {typeof(T).Name}: {value}");
    }

    public static bool TryParse(string? input, out T? result, out string? error)
    {
        result = null;
        error = null;

        if (string.IsNullOrWhiteSpace(input))
            return true;

        try
        {
            result = FromValue(input);
            return true;
        }
        catch (ArgumentException)
        {
            error = $"Valor inválido para {typeof(T).Name.Replace("Chamado", "")}: {input}";
            return false;
        }
    }
}
