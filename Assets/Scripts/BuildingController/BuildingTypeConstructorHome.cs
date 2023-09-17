using Assets.Scripts.BuildingController.Models;
using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController
{
    public class BuildingTypeConstructorHome : BuildingTypeConstructor
    {
        public BuildingTypeConstructorHome(string seed) : base(seed)
        {
        }

        public override Building GenerateBuilding()
        {
            var rooms = new List<Room>();

            var bedRoomCount = random.Next(1, 4);
            var kitchenCount = random.Next(1, 1);
            var bathroomCount = random.Next(1, 3);
            var livingRoomCount = random.Next(1, 4);

            for (var i = 0; i < bedRoomCount; i++)
            {
                var width = random.Next(2, 3);
                var depth = random.Next(2, 3);
                rooms.Add(roomConstructor.GenerateRoom(width, depth, RoomType.Bedroom));
            }

            for (var i = 0; i < kitchenCount; i++)
            {
                var width = random.Next(1, 3);
                var depth = random.Next(1, 2);
                rooms.Add(roomConstructor.GenerateRoom(width, depth, RoomType.Kitchen));
            }

            for (var i = 0; i < bathroomCount; i++)
            {
                var width = random.Next(1, 2);
                var depth = random.Next(1, 2);
                rooms.Add(roomConstructor.GenerateRoom(width, depth, RoomType.Bathroom));
            }

            for (var i = 0; i < livingRoomCount; i++)
            {
                var width = random.Next(2, 4);
                var depth = random.Next(2, 4);
                rooms.Add(roomConstructor.GenerateRoom(width, depth, RoomType.Living));
            }

            //Houses are put together in a snaking pattern with a max depth of 5
            var currentX = 0;
            var currentZ = 0;
            var randomizedRooms = rooms.OrderBy(x => random.Next()).ToList();

            var orderedRooms = new List<Room>();

            bool isGoingRight = true;

            int overClockCounter = 0;
            var maxX = 0;
            var maxZ = 0;

            while(randomizedRooms.Any() && overClockCounter < 1000)
            {
                overClockCounter++;
                if (currentX >= 5)
                {
                    currentX = 5;
                    currentZ += 1;
                    isGoingRight = false;
                }
                else if(currentX < 0)
                {
                    currentX = 0;
                    currentZ += 1;
                    isGoingRight = true;
                }

                if(currentZ > maxZ)
                {
                    maxZ = currentZ;
                }

                if(currentX > maxX)
                {
                    maxX = currentX;
                }

                if(SpaceOccupied(orderedRooms, currentX, currentZ))
                {
                    currentX = isGoingRight ? currentX + 1 : currentX - 1;
                    continue;
                }
                var room = randomizedRooms.First();
                
                if(RoomFits(orderedRooms, room, 5, currentX, currentZ))
                {
                    randomizedRooms.Remove(room);
                    room.X = currentX;
                    room.Z = currentZ;
                    orderedRooms.Add(room);
                    currentX = isGoingRight ? currentX + room.Width : currentX - room.Width;
                    continue;
                }
                else
                {
                    currentX = isGoingRight ? currentX + 1 : currentX - 1;
                }
            }

            var building = new Building()
            {
                Rooms = orderedRooms,
                X = 0,
                Y = 0,
                Z = 0,
                Width = maxX + 1,
                Depth = maxZ + 1,
                Height = 1
            };

            AddDoorsToRooms(building);

            return building;
        }

        public void AddDoorsToRooms(Building building)
        {
            foreach(var room in building.Rooms)
            {
                var maxDoors = 0;
                var doorsPlaced = DoorsInRoom(room);

                if (room.Type == RoomType.Bedroom)
                {
                    maxDoors = random.Next(2, 2);

                }
                else if(room.Type == RoomType.Bathroom)
                {
                    maxDoors = random.Next(1, 2);

                }
                else if(room.Type == RoomType.Kitchen)
                {
                    maxDoors = random.Next(2, 4);

                }
                else if(room.Type == RoomType.Living)
                {
                    maxDoors = random.Next(2,3);
                }

                var overloadCounter = 0;

                while (doorsPlaced < maxDoors && overloadCounter++ < 1000)
                {
                    var wallDirection = random.Next(0, 3);
                    if(wallDirection == 0)
                    {
                        var x = random.Next(0, room.Width - 1);
                        var z = 0;

                        var cell = room.Cells.FirstOrDefault(c => c.X == x && c.Z == z);
                        if (cell != null && cell.SouthWall && SpaceOccupied(building.Rooms, room.X + x, room.Z + z - 1))
                        {
                            cell.SouthWall = false;
                            
                            var otherSide = GetCellWithCoordinates(building.Rooms, room.X + x, room.Z + z - 1);
                            otherSide.NorthWall = false;

                            doorsPlaced++;
                        }
                    }
                    else if(wallDirection == 1)
                    {
                        var x = room.Width - 1;
                        var z = random.Next(0, room.Depth - 1);

                        var cell = room.Cells.FirstOrDefault(c => c.X == x && c.Z == z);
                        if (cell != null && cell.EastWall && SpaceOccupied(building.Rooms, room.X + x + 1, room.Z + z))
                        {
                            cell.EastWall = false;

                            var otherSide = GetCellWithCoordinates(building.Rooms, room.X + x + 1, room.Z + z);
                            otherSide.WestWall = false;

                            doorsPlaced++;
                        }
                    }
                    else if(wallDirection == 2)
                    {
                        var x = random.Next(0, room.Width - 1);
                        var z = room.Depth - 1;

                        var cell = room.Cells.FirstOrDefault(c => c.X == x && c.Z == z);
                        if (cell != null && cell.NorthWall && SpaceOccupied(building.Rooms, room.X + x, room.Z + z + 1))
                        {
                            cell.NorthWall = false;

                            var otherSide = GetCellWithCoordinates(building.Rooms, room.X + x, room.Z + z + 1);
                            otherSide.SouthWall = false;

                            doorsPlaced++;
                        }
                    }
                    else if(wallDirection == 3)
                    {
                        var x = 0;
                        var z = random.Next(0, room.Depth - 1);

                        var cell = room.Cells.FirstOrDefault(c => c.X == x && c.Z == z);
                        if (cell != null && cell.WestWall && SpaceOccupied(building.Rooms, room.X + x - 1, room.Z + z))
                        {
                            cell.WestWall = false;

                            var otherSide = GetCellWithCoordinates(building.Rooms, room.X + x - 1, room.Z + z);
                            otherSide.EastWall = false;

                            doorsPlaced++;
                        }
                    }
                }
            }
        }

        private int DoorsInRoom(Room room)
        {
            var southDoors = room.Cells.Count(c => c.X >= 0 && c.X <= room.Width - 1 && c.Z == 0 && !c.SouthWall);
            var northDoors = room.Cells.Count(c => c.X >= 0 && c.X <= room.Width - 1 && c.Z == room.Depth - 1 && !c.NorthWall);
            var eastDoors = room.Cells.Count(c => c.Z >= 0 && c.Z <= room.Depth - 1 && c.X == room.Width - 1 && !c.EastWall);
            var westDoors = room.Cells.Count(c => c.Z >= 0 && c.Z <= room.Depth - 1 && c.X == 0 && !c.WestWall);

            return southDoors + northDoors + eastDoors + westDoors;
        }

        private bool RoomFits(List<Room> currentRooms, Room room, int maxX, int currentX, int currentZ)
        {
            for(var x = currentX; x < currentX + room.Width; x++)
            {
                for(var z = currentZ; z < currentZ + room.Depth; z++)
                {
                    if (x >= maxX || SpaceOccupied(currentRooms, x, z))
                    {
                        return false;
                    }
                }
            }

            return true;
        }   

        private Room GetRoomWithCoorinates(List<Room> currentRooms, int X, int Z) {
            foreach (var room in currentRooms)
            {
                if (X >= room.X && X < room.X + room.Width && Z >= room.Z && Z < room.Z + room.Depth)
                {
                    return room;
                }
            }

            return null;
        }

        private RoomCell GetCellWithCoordinates(List<Room> currentRooms, int X, int Z)
        {
            var room = GetRoomWithCoorinates(currentRooms, X, Z);
            if(room != null)
            {
                return room.Cells.FirstOrDefault(c => room.X + c.X == X && room.Z + c.Z == Z);
            }

            return null;
        }

        private bool SpaceOccupied(List<Room> currentRooms, int X, int Z)
        {
            foreach(var room in currentRooms)
            {
                if(X >= room.X && X < room.X + room.Width && Z >= room.Z && Z < room.Z + room.Depth)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
