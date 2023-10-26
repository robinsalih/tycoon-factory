namespace Tycoon.Factory.Utils;

public static class Extensions
{
    public static async Task<IEnumerable<T2>> SelectAsync<T1, T2>(this IEnumerable<T1> l, Func<T1, Task<T2>> fn)
    {
        var results = new List<T2>();
        foreach (var item in l)
        {
            results.Add(await fn(item));
        }
        return results;
    }
}