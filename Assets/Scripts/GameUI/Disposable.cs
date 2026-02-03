using System;

public class Disposable : IDisposable
{
    private Action _onDispose;

    public Disposable(Action action)
    {
        _onDispose = action;
    }

    public void Dispose()
    {
        _onDispose.Invoke();
    }

    public void AddNewAction(Action action)
    {
        _onDispose += action;
    }
}
