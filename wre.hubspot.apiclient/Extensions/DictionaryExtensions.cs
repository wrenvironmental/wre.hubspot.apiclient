using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;

namespace wre.hubspot.apiclient.Extensions;

public static class DictionaryExtensions
{
    public static Dictionary<string, object> Data<T>(T obj, params Expression<Func<T, dynamic>>[] expressions)
    {
        var expCache = new ConcurrentDictionary<string, Delegate>();
        var dict = new Dictionary<string, object>();

        foreach (var exp in expressions)
        {
            var name = string.Empty;
            var body = exp.Body;

            if (body is UnaryExpression unaryExp)
                body = unaryExp.Operand;

            if (body is MemberExpression memberExp)
                name = memberExp.Member.Name;

            if (body is MethodCallExpression methodCallExp)
                name = methodCallExp.Method.Name;

            if (name == null)
                throw new InvalidExpressionException(
                    $"The expression '{exp.Body}' is invalid. You must supply an expression that references a property or a function of the type '{typeof(T)}'.");

            var key = typeof(T).FullName + "." + name;
            var func = (Func<T, dynamic>)expCache.GetOrAdd(key, _ => ((LambdaExpression)exp).Compile());

            dict[name] = func(obj);
        }

        return dict;
    }

}