using System;

public class ReactiveValue<T>
{
    private T _currentState;
    public event Action<T> OnChange;

    public void Signal()
    {
        OnChange?.Invoke(_currentState);
    }

    public T CurrentValue
    {
        get => _currentState;
        set
        {
            if (value.Equals(_currentState))
                return;
            else
            {
                _currentState = value;
                OnChange?.Invoke(_currentState);
            }
        }
    }
}
