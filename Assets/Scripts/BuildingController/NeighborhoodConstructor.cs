using Assets.Scripts.BuildingController.Models;
using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController
{
    public class NeighborhoodConstructor
    {
        private BuildingConstructor buildingConstructor;
        private string seed;
        private Random random;
        public NeighborhoodConstructor(string seed)
        {
            this.seed = seed;
            buildingConstructor = new BuildingConstructor(seed, BuildingType.House);
            random = new Random(seed.GetHashCode());
        }

        public Neighborhood GenerateNeightborhood()
        {
            var neighborhood = new Neighborhood()
            {
                NeighborhoodType = NeighborhoodType.Suburb,
                NeightborhoodBuildings = new List<NeightborhoodBuilding>()
            };
            var buildingsToGenerate = random.Next(80, 80);
            var xSpaceBetween = 3;
            var zSpaceBetween = 8;

            var currentX = 0;
            var currentZ = 0;

            var maxZ = 0;
            for (int rows = 0; rows < 2; rows++)
            {
                for (int i = 0; i < buildingsToGenerate / 2; i++)
                {
                    var neighborhoodBuilding = new NeightborhoodBuilding()
                    {
                        X = currentX,
                        Y = 0,
                        Z = currentZ,
                        Building = buildingConstructor.GenerateBuilding()
                    };
                    neighborhood.NeightborhoodBuildings.Add(neighborhoodBuilding);

                    currentX += neighborhoodBuilding.Building.Width + xSpaceBetween;
                    maxZ = Math.Max(maxZ, neighborhoodBuilding.Building.Depth);
                }
                currentX = 0;
                currentZ += maxZ + zSpaceBetween;
            }
            return neighborhood;
        }

        protected bool SpaceOccupied(int X, int Y, int Z, Neighborhood neighborhood)
        {
            foreach(var building in neighborhood.NeightborhoodBuildings)
            {
                if (X >= building.X && X <= building.X + building.Building.Width &&
                                       Z >= building.Z && Z <= building.Z + building.Building.Depth)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
