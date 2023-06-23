namespace DbBenchmarks.Sqlite;

public static class SqliteDbUtils
{
    public static readonly string DbPath;

    public const string DbFileName = "db.sqlite3";

    static SqliteDbUtils()
    {
        var binFolder = AppDomain.CurrentDomain.BaseDirectory;
        var rootFolder = new DirectoryInfo(binFolder);

        int exeDepth = 9;

        for (int i = 0; i < exeDepth; i++)
        {
            rootFolder = rootFolder.Parent;
        }

        Console.WriteLine(rootFolder);

        DbPath = Path.Join(rootFolder.FullName, "db", "sqlite", DbFileName);
    }
}
