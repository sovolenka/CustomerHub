namespace Data.Context;

public class SQLiteContextSingleton
{
    private static SQLiteContext context = new SQLiteContext();

    public static SQLiteContext Instance => context;
}
