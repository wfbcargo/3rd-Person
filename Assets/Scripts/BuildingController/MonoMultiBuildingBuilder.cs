using Assets.Scripts.BuildingController.RoomController;
using Assets.Scripts.BuildingController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.BuildingController.Models;

public class MonoMultiBuildingBuilder : MonoBuildingBuilder
{
    protected NeighborhoodConstructor neighborhoodConstructor;

    protected override void Awake()
    {
        base.Awake();
        neighborhoodConstructor = new NeighborhoodConstructor(Seed);
    }
    public override void OnStart()
    {
        var neighborhood = neighborhoodConstructor.GenerateNeightborhood();
        foreach (var building in neighborhood.NeightborhoodBuildings)
        {
            DrawBuilding(building.Building, building.X, building.Y, building.Z);
        }
    }
}
