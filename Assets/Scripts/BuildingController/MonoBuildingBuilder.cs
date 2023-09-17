using Assets.Scripts.BuildingController;
using Assets.Scripts.BuildingController.Models;
using Assets.Scripts.BuildingController.RoomController;
using Assets.Scripts.TerrainController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonoBuildingBuilder : MonoBehaviour
{
    public string Seed = "12345";

    protected BuildingConstructor buildingConstructor;
    public float UnitSize = 2.0f;

    protected virtual void Awake()
    {
        buildingConstructor = new BuildingConstructor(Seed, BuildingType.House);
    }
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnStart()
    {
        var building = buildingConstructor.GenerateBuilding();
        DrawBuilding(building, 0f, 0f, 0f);
    }

    public void DrawBuilding(Building building, float xOrigin, float yOrigin, float zOrigin)
    {
        foreach(var room in building.Rooms)
        {
            DrawRoom(room, xOrigin, yOrigin, zOrigin);
        }
    }

    private void DrawRoom(Room room, float xOrigin, float yOrigin, float zOrigin)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var width = room.Width * UnitSize;
        var depth = room.Depth * UnitSize;

        var baseX = (xOrigin + room.X) * UnitSize;
        var baseZ = (zOrigin + room.Z) * UnitSize;
        cube.transform.position = new Vector3( baseX + width / 2, yOrigin, baseZ + depth / 2);
        cube.transform.localScale = new Vector3(room.Width * UnitSize, 1, room.Depth * UnitSize);
        DrawWalls(room, xOrigin, yOrigin, zOrigin);
    }

    private void DrawWalls(Room room, float xOrigin, float yOrigin, float zOrigin)
    {
        var height = UnitSize * 2;

        var roomX = (xOrigin + room.X) * UnitSize;
        var baseZ = (zOrigin + room.Z) * UnitSize;
        var baseY = yOrigin;

        foreach(var cell in room.Cells)
        {
            var cellXDiff = cell.X * UnitSize;
            var cellZDiff = cell.Z * UnitSize;

            if(cell.NorthWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff + UnitSize / 2, baseY, baseZ + cellZDiff + UnitSize);
                wall.transform.localScale = new Vector3(UnitSize, height, 1);
            }

            if (cell.SouthWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff + UnitSize / 2, baseY, baseZ + cellZDiff);
                wall.transform.localScale = new Vector3(UnitSize, height, 1);
            }

            if (cell.EastWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff + UnitSize, baseY, baseZ + cellZDiff + UnitSize / 2);
                wall.transform.localScale = new Vector3(1, height, UnitSize);
            }

            if (cell.WestWall)
            {
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.position = new Vector3(roomX + cellXDiff, baseY, baseZ + cellZDiff + UnitSize / 2);
                wall.transform.localScale = new Vector3(1, height, UnitSize);
            }
        }
    }
}
