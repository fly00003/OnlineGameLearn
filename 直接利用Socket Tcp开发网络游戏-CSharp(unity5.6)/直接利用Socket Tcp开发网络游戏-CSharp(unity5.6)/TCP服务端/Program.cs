using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace TCP服务端
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StartServerAsync();
            Console.ReadKey();
        }
       static void StartServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("192.168.32.63");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 88);
            serverSocket.Bind(iPEndPoint);//绑定ip和端口号
            serverSocket.Listen(50);//开始监听，监听队列最多50个，当第51个客户端时需要排队等待直至监听队列中少于50个
            //，设置成0为不做任何限制，但服务器不一定受得了
            //Socket clientSocket = serverSocket.Accept();//接收一个客户端链接，只有接收到客户端才会向下执行
         
            serverSocket.BeginAccept(AcceptCallBack,serverSocket);
            //向客户端发送消息
         
        }
        static Message msg = new Message();
        private static void AcceptCallBack(IAsyncResult ar)
        {
            Socket severSocket = ar.AsyncState as Socket;
            Socket clientSocket = severSocket.EndAccept(ar);
            string msgStr = "Hello client!你好";//socket传递时要用byte数组，所以要转格式
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msgStr);//将字符串转为byte[],同样的GetString也可以将byte[]转为string
            clientSocket.Send(data);


            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);//异步通信，开始接受数据,存入dataBuffer，接收到数据后调用ReceiveCallBack，并将clientSocket作为参数传入
            severSocket.BeginAccept(AcceptCallBack,severSocket);
        }

        static  private void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;//将ar转为Socket类型
            try
            {
                clientSocket = ar.AsyncState as Socket;//将ar转为Socket类型
                int count = clientSocket.EndReceive(ar);//关闭时接收数据长度
                if (count == 0)
                {
                    clientSocket.Close(); return;
                }
                msg.AddCount(count);
                msg.ReadMessage();
                //string msgStr = Encoding.UTF8.GetString(dataBuffer, 0, count);
                //Console.WriteLine("从客户端接收数据：" + msgStr);
                //clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);//重新开启接收
                clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);//异步通信，开始接受数据

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
           
        }

        static byte[] dataBuffer = new byte[1024];
      static  void StartServerSync()//同步通信
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("192.168.32.63");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 88);
            serverSocket.Bind(iPEndPoint);//绑定ip和端口号
            serverSocket.Listen(50);//开始监听，监听队列最多50个，当第51个客户端时需要排队等待直至监听队列中少于50个
            //，设置成0为不做任何限制，但服务器不一定受得了
            Socket clientSocket = serverSocket.Accept();//接收一个客户端链接，只有接收到客户端才会向下执行

            //向客户端发送消息
            string msg = "Hello client!你好";//socket传递时要用byte数组，所以要转格式
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);//将字符串转为byte[],同样的GetString也可以将byte[]转为string
            clientSocket.Send(data);

            //接收客户端的一条消息
            byte[] dataBuffer = new byte[1024];
            int count = clientSocket.Receive(dataBuffer);//count为接受了多少个byte的信息
            string msgReceive = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count);
            Console.WriteLine(msgReceive);

            Console.ReadKey();

            clientSocket.Close();//关闭和客户端的连接，客户端自身是否关闭并不影响
            serverSocket.Close();//关闭服务器连接
        }

    }
}
