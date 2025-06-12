using MiF.SimpleMessenger;

namespace MiF.MessengerUnitTests;

public class MessengerTests
{
    [Fact]
    public void Subscribe_And_Send_Message_Should_Invoke_Subscriber()
    {
        // Arrange
        bool wasCalled = false;
        void Handler(string msg) => wasCalled = msg == "test";
        Messenger.Subscribe<string>(Handler);

        // Act
        Messenger.Send("test");

        // Assert
        Assert.True(wasCalled);

        // Cleanup
        Messenger.Unsubscribe<string>(Handler);
    }

    [Fact]
    public void Unsubscribe_Should_Remove_Subscriber()
    {
        // Arrange
        bool wasCalled = false;
        void Handler(string msg) => wasCalled = true;
        Messenger.Subscribe<string>(Handler);
        Messenger.Unsubscribe<string>(Handler);

        // Act
        Messenger.Send("test");

        // Assert
        Assert.False(wasCalled);
    }

    [Fact]
    public void Multiple_Subscribers_Should_All_Be_Called()
    {
        // Arrange
        int callCount = 0;
        void Handler1(string msg) => callCount++;
        void Handler2(string msg) => callCount++;
        Messenger.Subscribe<string>(Handler1);
        Messenger.Subscribe<string>(Handler2);

        // Act
        Messenger.Send("hello");

        // Assert
        Assert.Equal(2, callCount);

        // Cleanup
        Messenger.Unsubscribe<string>(Handler1);
        Messenger.Unsubscribe<string>(Handler2);
    }

    [Fact]
    public void Subscribe_Different_Message_Types_Should_Work_Independently()
    {
        // Arrange
        bool stringCalled = false;
        bool intCalled = false;
        void StringHandler(string msg) => stringCalled = true;
        void IntHandler(int msg) => intCalled = true;
        Messenger.Subscribe<string>(StringHandler);
        Messenger.Subscribe<int>(IntHandler);

        // Act
        Messenger.Send("abc");
        Messenger.Send(42);

        // Assert
        Assert.True(stringCalled);
        Assert.True(intCalled);

        // Cleanup
        Messenger.Unsubscribe<string>(StringHandler);
        Messenger.Unsubscribe<int>(IntHandler);
    }

    [Fact]
    public void Unsubscribe_Last_Subscriber_Removes_Type_Entry()
    {
        // Arrange
        void Handler(string msg) { }
        Messenger.Subscribe<string>(Handler);

        // Act
        Messenger.Unsubscribe<string>(Handler);

        // Assert
        // Envoi d'un message ne doit pas lever d'exception ni appeler Handler
        var exception = Record.Exception(() => Messenger.Send("test"));
        Assert.Null(exception);
    }
}