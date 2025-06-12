using MiF.SimpleMessenger;

namespace MiF.MessengerSample;

public class Subscriber
{
    public Subscriber()
    {
        Messenger.Subscribe<string>(OnMessageReceived);
    }

    private void OnMessageReceived(string message)
    {
        Console.WriteLine($"Message received: {message}");
    }

    public void Unsubscribe()
    {
        Messenger.Unsubscribe<string>(OnMessageReceived);
    }
}