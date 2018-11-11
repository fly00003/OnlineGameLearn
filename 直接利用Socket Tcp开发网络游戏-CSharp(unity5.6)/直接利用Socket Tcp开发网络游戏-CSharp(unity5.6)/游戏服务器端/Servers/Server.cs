using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using GameServer.Controller;
using Common;
namespace GameServer.Servers
{
    class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList=new List<Client>();
        private List<Room> roomList = new List<Room>();
        private ControllerManager controllerManager;
        public Server()
        { }
        public Server(string ipStr,int port)
        {
            SetIpAndPort(ipStr,port);
            controllerManager = new ControllerManager(this);
        }
        public void SetIpAndPort(string ipStr,int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }
        public void Start()
        {
           serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
           serverSocket.Bind(ipEndPoint);
           serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack,null); 
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket,this);
            clientList.Add(client);
            Console.WriteLine(client);
            serverSocket.BeginAccept(AcceptCallBack, null);
            //Console.ReadKey();
        }
        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }
        public void SendResponse(Client client,ActionCode actionCode,string data)
        {
            client.Send(actionCode,data);
            //Server给客户端响应,ControllerMessager给Server响应
        }
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)//client和server交互，server再和client交互，server相当于中介，让模块之间不那么混乱
        {
            controllerManager.HandleRequest(requestCode,actionCode,data,client);
        }
        public void CreateRoom(Client client)
        {
            Room room = new Room();
            room.AddClient(client);
            roomList.Add(room);
        }
    }
}
