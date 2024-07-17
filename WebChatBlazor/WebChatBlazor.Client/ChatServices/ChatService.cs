using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using System.Security.Claims;
using WebChatBlazor.Client.DTOs;
using WebChatBlazor.Client.Models;
using WebChatBlazor.Shared.Models;

namespace WebChatBlazor.Client.ChatServices;

public class ChatService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    public Action? InvokeChatDisplay { get; set; }
    public List<Chat> Chats { get; set; } = [];
    public List<AvailableUserDTO> ChatAvailableUsers { get; set; } = [];
    private readonly HubConnection hubConnection;
    public bool IsConnected { get; set; }
    private readonly HttpClient httpClient;

    public ChatService(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
    {
        this._authenticationStateProvider = authenticationStateProvider;
        this.httpClient = new HttpClient();
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
        hubConnection.On<List<AvailableUserDTO>>("NotifyAllClients", (users) =>
        {
            ChatAvailableUsers = users;
            InvokeChatDisplay?.Invoke();
        });
    }
    public async Task StartConectionAsync()
    {
        if (hubConnection.State == HubConnectionState.Disconnected)
        {
            await hubConnection.StartAsync();
            await GetUserAuthentication();
            Chats = await httpClient.GetFromJsonAsync<List<Chat>>("https://localhost:7219/api/chat");
            ChatAvailableUsers = await httpClient.GetFromJsonAsync<List<AvailableUserDTO>>("https://localhost:7219/api/chat/users");
            IsConnected = hubConnection!.State == HubConnectionState.Connected;
        }

    }

    private async Task GetUserAuthentication()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            await hubConnection!.SendAsync("AddAvailableUserAsync", new AvailableUser()
            {
                UserId = user.Claims.FirstOrDefault(_ => _.Type==ClaimTypes.NameIdentifier)!.Value
            });
        }
    }

    public async Task SendChat(Chat chat)
    {
        await hubConnection!.SendAsync("SendMessage", chat);
        chat.MessageInput = null;
    }



}
