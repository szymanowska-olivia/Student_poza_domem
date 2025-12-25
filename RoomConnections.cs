using System;
using System.Linq;


    public class RoomConnection
    {
        public string Direction { get; }
        public Room TargetRoom { get; }

        public RoomConnection(string direction, Room targetRoom)
        {
            Direction = direction;
            TargetRoom = targetRoom;
        }
        public virtual bool CanPass(Player player)
        {
            return true;
        }
    }

