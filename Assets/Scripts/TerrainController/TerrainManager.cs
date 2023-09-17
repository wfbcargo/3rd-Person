using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TerrainController
{
    internal class TerrainManager
    {
        private readonly List<TerrainConstructor> HardCodedTerrains = new List<TerrainConstructor>()
        {
            new TerrainConstructor(Resources.Load<Terrain>("Terrains/Terrain1x1A"), new TerrainDimensions()
            {
                Width = 1,
                Depth = 1,
                BottomHeight = 1,
                TopHeight = 1,
                LeftHeight = 1,
                RightHeight = 1
            }, 0, 0),
            new TerrainConstructor(Resources.Load<Terrain>("Terrains/Terrain1x1B"), new TerrainDimensions()
            {
                Width = 1,
                Depth = 1,
                BottomHeight = 1,
                TopHeight = 1,
                LeftHeight = 1,
                RightHeight = 1
            }, 0, 0),
            new TerrainConstructor(Resources.Load<Terrain>("Terrains/Terrain1x1C"), new TerrainDimensions()
            {
                Width = 1,
                Depth = 1,
                BottomHeight = 1,
                TopHeight = 1,
                LeftHeight = 1,
                RightHeight = 1
            }, 0, 0)
        };

        private System.Random random = new System.Random();

        public TerrainManager()
        {
        }

        public TerrainManager(string seed)
        {
            random = new System.Random(seed.GetHashCode());
        }

        public List<TerrainConstructor> GenerateSmallCellMap(int width, int depth)
        {
            var terrainConstructors = new List<TerrainConstructor>();
            for (int x = 0; x < width; x ++)
            {
                for (int y = 0; y < depth; y++)
                {
                    var terrain = GetRandomTerrain();
                    terrainConstructors.Add(new TerrainConstructor(terrain.Terrain, terrain.TerrainDimensions, x, y));
                }
            }

            return terrainConstructors;
        }

        private TerrainConstructor GetRandomTerrain()
        {
            var randomTerrain = HardCodedTerrains[random.Next(0, HardCodedTerrains.Count)];
            return randomTerrain;
        }
    }
}
