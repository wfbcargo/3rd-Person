using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController.Models
{
    public class Building
    {
        public int X { get; set; }

        public int Y { get; set; }
        public int Z { get; set; }
        public List<Room> Rooms { get; set; }

        public int Width { get; set; }

        public int Depth { get; set; }

        public int Height { get; set; }
    }
}
