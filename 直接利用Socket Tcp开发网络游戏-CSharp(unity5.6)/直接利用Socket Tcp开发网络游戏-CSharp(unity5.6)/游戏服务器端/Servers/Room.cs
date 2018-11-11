using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battle,
        End
    }
    class Room
    {
        private List<Client> clientRoom = new List<Client>();//这个应该是记录当前房间的人数，满两人就可以开始
        private RoomState state = RoomState.WaitingJoin;
        public void AddClient(Client client)
        {
            clientRoom.Add(client);
        }
    }
}
