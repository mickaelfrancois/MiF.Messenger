using MiF.MessengerSample;

var subscriber = new Subscriber();
var publisher = new Publisher();

publisher.PublishMessage(); // Output: Message received: Hello from Publisher!

subscriber.Unsubscribe();
publisher.PublishMessage(); // No output, as the subscriber has unsubscribed.  
