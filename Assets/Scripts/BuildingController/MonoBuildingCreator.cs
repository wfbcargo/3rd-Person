using Assets.Scripts.BuildingController;
using Assets.Scripts.BuildingController.RoomController;
using Assets.Scripts.TerrainController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBuildingCreator : MonoBehaviour
{
    public string Seed = "12345";

    private BuildingConstructor buildingConstructor;
    private float UnitSize = 10.0f;

    private void Awake()
    {
        buildingConstructor = new BuildingConstructor(Seed);
    }
    // Start is called before the first frame update
    void Start()
    {
        var building = buildingConstructor.GenerateBuilding(BuildingType.House);
        DrawBuilding(building);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawBuilding(Building building)
    {
        foreach(var room in building.Rooms)
        {
            DrawRoom(room);
        }
    }

    private void DrawRoom(Room room)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var width = room.Width * UnitSize;
        var depth = room.Depth * UnitSize;

        var baseX = room.X * UnitSize;
        var baseZ = room.Z * UnitSize;
        cube.transform.position = new Vector3(baseX + width / 2, 0, baseZ + depth / 2);
        cube.transform.localScale = new Vector3(room.Width * UnitSize, 1, room.Depth * UnitSize);
        DrawWalls(room);
    }

    private void DrawWalls(Room room)
    {
        var height = 20f;

        var roomX = room.X * UnitSize;
        var baseZ = room.Z * UnitSize;

        foreach(var cell in room.Cells)
        {
            var cellXDiff = cell.X * UnitSize;
            var cellZDiff = cell.Z * UnitSize;

            if(cell.NorthWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff + UnitSize / 2, 0, baseZ + cellZDiff + UnitSize);
                wall.transform.localScale = new Vector3(UnitSize, height, 1);
            }

            if (cell.SouthWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff + UnitSize / 2, 0, baseZ + cellZDiff);
                wall.transform.localScale = new Vector3(UnitSize, height, 1);
            }

            if (cell.EastWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff + UnitSize, 0, baseZ + cellZDiff + UnitSize / 2);
                wall.transform.localScale = new Vector3(1, height, UnitSize);
            }

            if (cell.WestWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff, 0, baseZ + cellZDiff + UnitSize / 2);
                wall.transform.localScale = new Vector3(1, height, UnitSize);
            }
        }
    }
}
