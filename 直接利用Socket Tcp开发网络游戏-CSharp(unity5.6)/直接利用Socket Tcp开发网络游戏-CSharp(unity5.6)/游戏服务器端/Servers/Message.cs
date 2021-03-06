﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace GameServer.Servers
{
    class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;
        //public void AddCount(int count)
        //{
        //    startIndex += count;
        //}
        public byte[] Data
        {
            get { return data; }
        }
        public int StartIndex
        {
            get { return startIndex; }
        }
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }
        public void ReadMessage(int newDataAmount,Action<RequestCode,ActionCode,string> processDataCallback)//增加函数回调用来处理解析出来的信息，这样ReadMessage只负责解析信息
        {
            startIndex += newDataAmount;
            while (true)
            {
                if (startIndex <= 4)
                {
                    return;
                }
                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)
                {
                    RequestCode requestCode =(RequestCode)BitConverter.ToInt32(data,4);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data,8);
                    string s = Encoding.UTF8.GetString(data, 12, count-8);
                    //Console.WriteLine("解析出来一条数据：" + s);
                    processDataCallback(requestCode,actionCode,s);
                    Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }
        public static byte[] PackData(ActionCode actionCode,string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
        }
    }
}
