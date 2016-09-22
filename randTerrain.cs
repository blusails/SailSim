using UnityEngine;
using System.Collections;
using UnityEditor;
public class randTerrain
{
    Terrain terrain;
    TerrainData tData;
    int xRes;
    int yRes;

    float[,] heightMap;

    [MenuItem("GameObject/Create Other/rand terrain...")]
    static public void randomizeTerrain()
    {
        GameObject tObj = GameObject.Find("fuckinTerrain");
        Terrain terrain = tObj.transform.GetComponent<Terrain>();
        TerrainData tData = terrain.terrainData;

        int xRes = tData.heightmapHeight;
        int yRes = tData.heightmapWidth;
        float[,] heightMap = new float[xRes, yRes];


        
        float xStart = Random.Range(1.0f, 100.0f);
        float yStart = Random.Range(1.0f, 100.0f);
        Debug.Log(xStart);
        for (int i = 0; i < xRes; i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                heightMap[i, j] = Mathf.PerlinNoise(i*.007f+xStart, j*.007f+yStart);
            }

        }
        tData.SetHeights(0, 0, heightMap);

    }

    float[,] genHeightMap(int xRes, int yRes)

    {
        float[,] map = new float[xRes, yRes];
        int xStart = (int)Random.Range(0.0f, 1000000000000000000000f);
        int yStart = (int)Random.Range(0.0f, 1000000000000000000000f);
        for (int i = 0; i< xRes; i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                map[i, j] = Mathf.PerlinNoise(i + xStart, j + yStart);
            }

        }
        return map;
    }
}

