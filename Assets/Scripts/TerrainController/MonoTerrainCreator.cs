using Assets.Scripts.TerrainController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTerrainCreator : MonoBehaviour
{
    public int Width = 10;
    public int Depth = 10;
    public string Seed = "12345";

    private TerrainManager terrainManager;
    private float UnitSize = 100.0f;

    private void Awake()
    {
        terrainManager = new TerrainManager(Seed);
    }
    // Start is called before the first frame update
    void Start()
    {
        var terrainConstructors = terrainManager.GenerateSmallCellMap(Width, Depth);
        ProcessTerrainConstructors(terrainConstructors);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProcessTerrainConstructors(List<TerrainConstructor> terrainConstructors)
    {
        foreach (var terrainConstructor in terrainConstructors)
        {
            var terrain = Instantiate(terrainConstructor.Terrain);
            terrain.transform.position = new Vector3(terrainConstructor.X * UnitSize, 0, terrainConstructor.Y * UnitSize);
        }
    }
}
