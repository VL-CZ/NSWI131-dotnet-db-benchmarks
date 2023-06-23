namespace DbBenchmarks;

static class ListExtensions
{
    public static T GetRandom<T>(this List<T> items)
    {
        return items[Random.Shared.Next(items.Count)];
    }
}
