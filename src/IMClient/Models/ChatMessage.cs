using System;

namespace IMClient.Models;

public class ChatMessage
{
    public ChatMessage(string sender, string content, DateTime timestamp, bool isMine)
    {
        Sender = sender;
        Content = content;
        Timestamp = timestamp;
        IsMine = isMine;
    }

    public string Sender { get; }

    public string Content { get; }

    public DateTime Timestamp { get; }

    public bool IsMine { get; }
}
