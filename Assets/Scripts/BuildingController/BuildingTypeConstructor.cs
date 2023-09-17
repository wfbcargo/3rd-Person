using Assets.Scripts.BuildingController.RoomController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.BuildingController
{
    public abstract class BuildingTypeConstructor
    {
        protected RoomConstructor roomConstructor;
        protected Random random;
        public BuildingTypeConstructor(string seed)
        {
            roomConstructor = new RoomConstructor();
            random = new Random(seed.GetHashCode());
        }

        public virtual Building GenerateBuilding()
        {
            return new Building();
        }
    }
}
