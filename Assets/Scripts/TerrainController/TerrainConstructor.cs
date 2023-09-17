using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TerrainController
{
    public class TerrainConstructor
    {
        public Terrain Terrain;
        public TerrainDimensions TerrainDimensions;
        public int X;
        public int Y;

        public TerrainConstructor(Terrain terrain, TerrainDimensions terrainDimensions, int x, int y)
        {
            Terrain = terrain;
            TerrainDimensions = terrainDimensions;
            X = x;
            Y = y;
        }
    }
}
