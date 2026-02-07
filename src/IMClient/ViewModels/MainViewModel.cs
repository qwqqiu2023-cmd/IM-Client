using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using IMClient.Models;

namespace IMClient.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private Conversation? _selectedConversation;
    private string _newMessageText = string.Empty;

    public MainViewModel()
    {
        Conversations = new ObservableCollection<Conversation>
        {
            new("团队群聊", new ObservableCollection<ChatMessage>
            {
                new("Alex", "早上好，今天我们继续同步需求。", DateTime.Today.AddHours(9).AddMinutes(32), false),
                new("你", "好的，我准备好了。", DateTime.Today.AddHours(9).AddMinutes(34), true),
                new("Mia", "我会把会议纪要发到群里。", DateTime.Today.AddHours(9).AddMinutes(35), false)
            }),
            new("产品讨论", new ObservableCollection<ChatMessage>
            {
                new("产品经理", "下午两点评审需求。", DateTime.Today.AddHours(10), false)
            }),
            new("私聊 - Luna", new ObservableCollection<ChatMessage>
            {
                new("Luna", "晚上一起复盘吗？", DateTime.Today.AddHours(11), false)
            })
        };

        SelectedConversation = Conversations[0];
        SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
    }

    public ObservableCollection<Conversation> Conversations { get; }

    public Conversation? SelectedConversation
    {
        get => _selectedConversation;
        set
        {
            if (SetField(ref _selectedConversation, value))
            {
                OnPropertyChanged(nameof(HasConversation));
                RaiseSendMessageCanExecuteChanged();
            }
        }
    }

    public bool HasConversation => SelectedConversation != null;

    public string NewMessageText
    {
        get => _newMessageText;
        set
        {
            if (SetField(ref _newMessageText, value))
            {
                RaiseSendMessageCanExecuteChanged();
            }
        }
    }

    public ICommand SendMessageCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SendMessage()
    {
        if (SelectedConversation == null)
        {
            return;
        }

        var trimmed = NewMessageText.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return;
        }

        SelectedConversation.Messages.Add(new ChatMessage("你", trimmed, DateTime.Now, true));
        NewMessageText = string.Empty;
    }

    private bool CanSendMessage()
    {
        return SelectedConversation != null && !string.IsNullOrWhiteSpace(NewMessageText);
    }

    private void RaiseSendMessageCanExecuteChanged()
    {
        if (SendMessageCommand is RelayCommand relayCommand)
        {
            relayCommand.RaiseCanExecuteChanged();
        }
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void OnPropertyChanged(string? propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
