using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;
public class ClientManager :BaseManager{
    private const string IP = "192.168.32.63";
    private const int PORT = 6688;
    private Socket clientSocket;
    private Message msg = new Message();
    public ClientManager(GameFacade facade) : base(facade)
    {
    }
    public override void OnInit()
    {
        base.OnInit();

         clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP,PORT);
            Start();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务器，请检查您的网络！！"+e);
        }
    }
    private void Start()
    {
        clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize,SocketFlags.None,ReceiveCallback,null);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int count = clientSocket.EndReceive(ar);

            msg.ReadMessage(count,OnProcessDataCallback);
            Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnProcessDataCallback(ActionCode actionCode, string data)
    {
        facade.HandleReponse(actionCode,data);// 服务器响应消息的处理
    }

    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        byte[] bytes = Message.PackData(requestCode,actionCode,data);
        clientSocket.Send(bytes);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭跟服务器的连接！！"+e);
        }
    }
}
