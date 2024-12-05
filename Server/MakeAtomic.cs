namespace GameServer.Server;

public class MakeAtomic<TAny> : IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly TAny _atomicObject;

    public MakeAtomic(TAny atomicObject)
    {
        _atomicObject = atomicObject;
    }

    public async Task ExecuteAsync(Func<TAny, Task> action)
    {
        await _semaphore.WaitAsync();
        try
        {
            await action(_atomicObject);
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