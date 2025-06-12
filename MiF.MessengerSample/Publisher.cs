using MiF.SimpleMessenger;

namespace MiF.MessengerSample;

public class Publisher
{
    public void PublishMessage()
    {
        Messenger.Send("Hello from Publisher!");
    }
}
