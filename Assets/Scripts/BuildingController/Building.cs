using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController
{
    public class Building
    {
        public int X { get; set; }
        public int Z { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
