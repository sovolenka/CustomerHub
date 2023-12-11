using Data.Context;

namespace Test.Services;

public abstract class TestsBase : IDisposable
{
    protected readonly SQLiteContext ContextProxy;

    protected TestsBase(string db)
    {
        ContextProxy = new SQLiteContext(db);
    }
    
    public void Dispose()
    {
        ContextProxy.DropDatabase();
    }
}
