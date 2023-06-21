namespace DbBenchmarks;

internal static class SqliteDbUtils
{
    public static readonly string DbPath;

    public const string DbFileName = "db.sqlite3";

    static SqliteDbUtils()
    {
        var binFolder = AppDomain.CurrentDomain.BaseDirectory;
        var rootFolder = Directory.GetParent(binFolder).Parent.Parent.Parent.Parent.Parent.FullName;

        DbPath = Path.Join(rootFolder, "db", DbFileName);
    }
}
