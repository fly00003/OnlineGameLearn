﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Text;
using System.Linq;

public class Message {

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
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallback)//增加函数回调用来处理解析出来的信息，这样ReadMessage只负责解析信息
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
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
               // ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                //Console.WriteLine("解析出来一条数据：" + s);
                processDataCallback(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }
    //public static byte[] PackData(RequestCode requestCode, string data)
    //{
    //    byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestCodeBytes.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
    //}
    public static byte[] PackData(RequestCode requestCode,ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + actionCodeBytes.Length+dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        //return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
        return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().
            Concat(actionCodeBytes).ToArray<byte>().
            Concat(dataBytes).ToArray<byte>();
    }
}
