using ChatLib.Events;
using ChatLib.Handlers;
using ChatLib.Models;
using ChatLib.Sockets;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

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

        private void Connected(object? sender, ChatEventArgs e)
        {
            var hub = CreateNewStateChatHub(e.Hub, ChatState.Connect);

            var userList = _roomManager.Add(e.ClientHandler);
            _roomManager.SendToMyRoom(hub);

            lbxClients.Items.Add(e.Hub);
            AddClientMessageList(hub);

            SendUserListToRoomClients(hub.RoomId);

            // 만약 방에 4명이 있다면 제시어를 할당하고 전송
            if (userList.Count == 4)
            {
                AssignAndSendWords(userList, hub.RoomId);
            }
        }

        private void Disconnected(object? sender, ChatEventArgs e)
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

        private void SendUserListToRoomClients(int roomId)
        {
            var users = _roomManager.GetRoomUsers(roomId);
            var userNames = users.Select(u => u.InitialData.UserName).ToList();
            var responseHub = new ChatHub
            {
                RoomId = roomId,
                State = ChatState.Message,
                Message = "USER_LIST:" + string.Join(",", userNames)
            };
            foreach (var client in users)
            {
                client.Send(responseHub);
            }
        }

        private List<string[]> ReadCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            return lines.Select(line => line.Split(','))
                        .Where(columns => columns.Length >= 3)
                        .ToList();
        }

        private void AssignAndSendWords(List<ClientHandler> users, int roomId)
        {
            var csvData = ReadCsv("C:\\Users\\ibsun\\OneDrive\\바탕 화면\\APP\\server\\Server\\category\\categories_v2.csv");
            var random = new Random();
            var selectedRow = csvData[random.Next(csvData.Count)];

            var word1 = selectedRow[1];
            var word2 = selectedRow[2];

            var liarIndex = random.Next(users.Count);
            for (int i = 0; i < users.Count; i++)
            {
                var assignedWord = i == liarIndex ? word1 : word2;
                var responseHub = new ChatHub
                {
                    RoomId = roomId,
                    State = ChatState.Message,
                    Message = "WORD:" + assignedWord
                };
                Console.WriteLine("RoomID:"+responseHub.RoomId+" Massage: "+responseHub.Message);
                users[i].Send(responseHub);
            }
        }
    }
}
