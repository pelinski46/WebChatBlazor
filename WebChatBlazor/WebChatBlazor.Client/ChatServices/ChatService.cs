using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using WebChatBlazor.Shared.Models;

namespace WebChatBlazor.Client.ChatServices;

public class ChatService
{
    public Action? InvokeChatDisplay {  get; set; }
    public List<Chat> Chats { get; set; } = [];
    private readonly HubConnection hubConnection;
    public bool IsConnected { get; set; }

    public ChatService (NavigationManager navigationManager)
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();
    }
    public void ReceiveMessage()
    {
        hubConnection.On<Chat>("ReceiveMessage", (chat) =>
        {
            Chats.Add(chat);
            InvokeChatDisplay?.Invoke();
        });
    }
    public async Task StartConectionAsync()
    {
        await hubConnection.StartAsync();
        IsConnected = hubConnection!.State == HubConnectionState.Connected;
    }
    public Task SendChat (Chat chat) =>
        hubConnection!.SendAsync("SendMessage", chat);
    
        
}
