using Assets.Scripts.BuildingController.Models;
using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController
{
    public class BuildingConstructor
    {
        private RoomConstructor roomConstructor;
        private BuildingTypeConstructor buildingTypeConstructor;
        private string seed;

        public BuildingConstructor(string seed, BuildingType buildingType)
        {
            roomConstructor = new RoomConstructor();
            this.seed = seed;
            buildingTypeConstructor = GetBuildingTypeConstructor(buildingType);
        }

        public Building GenerateBuilding()
        {
            var building = buildingTypeConstructor.GenerateBuilding();
            return building;
        }

        private BuildingTypeConstructor GetBuildingTypeConstructor(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.House:
                    return new BuildingTypeConstructorHome(seed);
                case BuildingType.Garage:
                case BuildingType.Store:
                default:
                    return new BuildingTypeConstructorHome(seed);
            }
        }
    }
}
