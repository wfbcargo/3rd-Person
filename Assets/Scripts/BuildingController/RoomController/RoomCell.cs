using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController.RoomController
{
    public class RoomCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool NorthWall { get; set; }
        public bool SouthWall { get; set; }
        public bool EastWall { get; set; }
        public bool WestWall { get; set; }
        public bool Ceiling { get; set; }
        public bool Floor { get; set; }
    }
}
