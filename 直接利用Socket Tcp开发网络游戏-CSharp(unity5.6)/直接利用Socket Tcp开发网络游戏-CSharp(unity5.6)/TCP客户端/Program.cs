using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace TCP客户端
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.32.63"), 88));//连接到服务器即可

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);//当建立连接的时候服务器会向客户端发送一条信息，客户端在这里接收，而且只有接收到信息才会继续向下
            Console.WriteLine(Encoding.UTF8.GetString(data,0,count));
            for (int i = 0; i < 100; i++)
            {
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }
            while (true)
            {            
                string s = Console.ReadLine();
                if (s == "close")
                {
                    clientSocket.Close(); return;
                }
                clientSocket.Send(Message.GetBytes(s));
            }

            Console.ReadKey();
            clientSocket.Close();
        }
    }
}
