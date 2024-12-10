namespace GameServer.Server;

public class MakeAtomic<TAny>(TAny atomicObject) : IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task ExecuteAsync(Func<TAny, Task> action)
    {
        await _semaphore.WaitAsync();
        try
        {
            await action(atomicObject);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        _semaphore.Dispose();
    }
}