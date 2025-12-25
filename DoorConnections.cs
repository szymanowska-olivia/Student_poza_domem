using System;
using System.Linq;


    public class DoorConnection : RoomConnection
    {
        public bool IsLocked { get; private set; }
        public string RequiredKeyName { get; }

        public DoorConnection(string direction, Room targetRoom, string keyName)
            : base(direction, targetRoom)
        {
            RequiredKeyName = keyName;
            IsLocked = true;
        }

        public override bool CanPass(Player player)
        {
            return !IsLocked || player.Inventory.Exists(item =>
                string.Equals(item.Name, RequiredKeyName, StringComparison.OrdinalIgnoreCase));
        }

        public void Open(Player player)
        {
            if (CanPass(player))
            {
                IsLocked = false;
                Console.WriteLine("Drzwi zostały otwarte.");
            }
            else
            {
                Console.WriteLine("Drzwi są zamknięte. Potrzebujesz odpowiedniego klucza.");
            }
        }

        public void Close()
        {
            IsLocked = true;
            Console.WriteLine("Drzwi zostały zamknięte.");
        }
    }

