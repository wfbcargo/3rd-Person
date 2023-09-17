using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController.Models
{
    public class Neighborhood
    {
        public NeighborhoodType NeighborhoodType { get; set; }
        public List<NeightborhoodBuilding> NeightborhoodBuildings { get; set; }
    }
}
