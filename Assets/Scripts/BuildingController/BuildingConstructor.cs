using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController
{
    internal class BuildingConstructor
    {
        private RoomConstructor roomConstructor;
        private string seed;

        public BuildingConstructor(string seed)
        {
            roomConstructor = new RoomConstructor();
            this.seed = seed;
        }

        public Building GenerateBuilding(BuildingType buildingType)
        {
            var buildingTypeConstructor = GetBuildingTypeConstructor(buildingType);
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
