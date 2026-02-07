using System.Collections.ObjectModel;

namespace IMClient.Models;

public class Conversation
{
    public Conversation(string name, ObservableCollection<ChatMessage> messages)
    {
        Name = name;
        Messages = messages;
    }

    public string Name { get; }

    public ObservableCollection<ChatMessage> Messages { get; }
}
