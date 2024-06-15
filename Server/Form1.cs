using ChatLib.Events;
using ChatLib.Handlers;
using ChatLib.Models;
using ChatLib.Sockets;
using System.Net;

namespace WinFormServer
{
  public partial class Form1 : Form
  {
    private ChatServer _server;
    private ClientRoomManager _roomManager;

    private ChatHub CreateNewStateChatHub(ChatHub hub, ChatState state)
    {
      return new ChatHub
      {
        RoomId = hub.RoomId,
        UserName = hub.UserName,
        State = state,
      };
    }

    private void AddClientMessageList(ChatHub hub)
    {
      string message = hub.State switch
      {
        ChatState.Connect => $"★ 접속 ★ {hub} ★",
        ChatState.Disconnect => $"★ 접속 종료 ★ {hub} ★",
        _ => $"{hub}: {hub.Message}"
      };
      lbxMsg.Items.Add(message);
    }

    private void Connected(object? sender, ChatEventArgs e) // 클라 접속하는 순간
    {
      var hub = CreateNewStateChatHub(e.Hub, ChatState.Connect);

      int count = _roomManager.Add(e.ClientHandler); // 인원 수, 0은 인원 가득참
      if(count != 0){
        _roomManager.SendToMyRoom(hub);

        lbxClients.Items.Add(e.Hub); // server client 정보 추가
        AddClientMessageList(hub); // 접속 메시지 전송

        if(count == 4){
          // 게임시작 및 제시어 제공
        }
      }
    }

    private void Disconnected(object? sender, ChatEventArgs e)  // 클라 접속 하는 순간
     {
      var hub = CreateNewStateChatHub(e.Hub, ChatState.Disconnect);

      _roomManager.Remove(e.ClientHandler);
      _roomManager.SendToMyRoom(hub);

      lbxClients.Items.Remove(e.Hub);
      AddClientMessageList(hub);
    }

    private void Received(object? sender, ChatEventArgs e)
    {
      _roomManager.SendToMyRoom(e.Hub);

      AddClientMessageList(e.Hub);
    }

    private void RunningStateChanged(bool isRunning)
    {
      btnStart.Enabled = !isRunning;
      btnStop.Enabled = isRunning;
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
      _ = _server.StartAsync();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
      _server.Stop();
    }

    public Form1()
    {
      InitializeComponent();

      _roomManager = new ClientRoomManager();
      _server = new ChatServer(IPAddress.Parse("127.0.0.1"), 8080);
      _server.Connected += Connected;
      _server.Disconnected += Disconnected;
      _server.Received += Received;
      _server.RunningStateChanged += RunningStateChanged;

      btnStart.Click += BtnStart_Click;
      btnStop.Click += BtnStop_Click;
    }
  }
}