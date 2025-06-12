namespace MiF.SimpleMessenger;

public class Messenger : IMessenger
{
    private static readonly Dictionary<Type, List<Delegate>> _subscribers = [];

    public static void Subscribe<TMessage>(Action<TMessage> action)
    {
        Type messageType = typeof(TMessage);

        if (!_subscribers.ContainsKey(messageType))
            _subscribers[messageType] = new List<Delegate>();

        _subscribers[messageType].Add(action);
    }

    public static void Unsubscribe<TMessage>(Action<TMessage> action)
    {
        Type messageType = typeof(TMessage);

        if (_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType].Remove(action);

            if (_subscribers[messageType].Count == 0)
                _subscribers.Remove(messageType);
        }
    }

    public static void Send<TMessage>(TMessage message)
    {
        Type messageType = typeof(TMessage);

        if (_subscribers.ContainsKey(messageType))
        {
            foreach (var action in _subscribers[messageType])
                ((Action<TMessage>)action)?.Invoke(message);
        }
    }
}
