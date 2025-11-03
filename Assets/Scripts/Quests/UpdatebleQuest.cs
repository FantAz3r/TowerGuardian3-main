using System;

public abstract class UpdatableQuest : Quest, IUpdatableQuest
{
    private int _currentValue = 0;
    public int Goal { get; private set; }
    public event Action<int> Updated;

    public void Set(int goal)
    {
        Goal = goal;
        _currentValue = 0;
    }

    public virtual void UpdateProgress()
    {
        _currentValue++;
        Updated?.Invoke(_currentValue);

        if (_currentValue >= Goal)
        {
            Complete();
        }
    }
}
