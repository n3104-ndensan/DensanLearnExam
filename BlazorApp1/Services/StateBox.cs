namespace BlazorApp1.Services;

public class StateBox<T> : IStateBox<T>
{
    private T? _state;

    public T? State
    {
        get => _state;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_state, value))
            {
                _state = value;
            }
        }
    }

    public bool HasState => _state is not null;
}
