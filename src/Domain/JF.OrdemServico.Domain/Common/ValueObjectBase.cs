using System.Reflection;

namespace JF.OrdemServico.Domain.Common;

public abstract class ValueObjectBase
{
    public string Value { get; private set; }

    public string Name { get; private set; }

    protected ValueObjectBase(string value, string name)
    {
        Value = value;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : ValueObjectBase
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public static T FromValue<T>(string value) where T : ValueObjectBase
    {
        return GetAll<T>().FirstOrDefault(x => x.Value.Equals(value)) ?? throw new ArgumentException($"Valor inválido para {typeof(T).Name}: {value}");
    }
}
