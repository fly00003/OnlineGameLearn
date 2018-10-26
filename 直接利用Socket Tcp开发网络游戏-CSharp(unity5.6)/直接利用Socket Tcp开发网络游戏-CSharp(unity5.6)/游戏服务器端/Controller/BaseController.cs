using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
namespace GameServer.Controller
{
    abstract class BaseController
    {
        RequestCode requestCode = RequestCode.None;
    public RequestCode RequestCode {
            get {
                return requestCode;
            }
        }
        public virtual string DefaultHandle(string data,Client client,Server server )//data是解析出来的来自客户端的数据
        {
            return null;//返回值是返回给客户端的数据，如果是返回为null，则不发送给客户端
        }
    }
}
