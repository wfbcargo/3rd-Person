using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController.RoomController
{
    public class RoomConstructor
    {
        public Room GenerateRoom(int width, int depth, RoomType roomType)
        {
            var room = new Room()
            {
                Width = width,
                Depth = depth,
                Height = 1,
                Cells = new List<RoomCell>(),
                Type = roomType
            };

            for (int x = 0; x < room.Width; x++)
            {
                for (int z = 0; z < room.Depth; z++)
                {
                    var roomCell = new RoomCell()
                    {
                        X = x,
                        Z = z,
                        Y = 0,
                    };

                    if(x == 0)
                    {
                        roomCell.WestWall = true;
                    }
                    if(x == room.Width - 1)
                    {
                        roomCell.EastWall = true;
                    }
                    if(z == 0)
                    {
                        roomCell.SouthWall = true;
                    }
                    if(z == room.Depth - 1)
                    {
                        roomCell.NorthWall = true;
                    }

                    room.Cells.Add(roomCell);
                }
            }

            return room;
        }
    }
}
