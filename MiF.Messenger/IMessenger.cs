namespace MiF.SimpleMessenger;

public interface IMessenger
{
    static abstract void Send<TMessage>(TMessage message);

    static abstract void Subscribe<TMessage>(Action<TMessage> action);

    static abstract void Unsubscribe<TMessage>(Action<TMessage> action);
}