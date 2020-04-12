﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

  public enum DrawMode
  {
    NoiseMap, ColorMap
  }

  public DrawMode drawMode;
  
  
  public int mapWidth;

  public int mapHeight;
  public float noiseScale;
  public bool autoUpdate = false;

  public TerrainType[] regions;

  public int seed;
  public Vector2 offset;
  public int octaves;
  [Range(0, 1)]
  public float persistance;
  public float lacunarity;

  public void GenerateMap()
  {
    float[,] noiseMap = Noise.GenerateNoiseMap(
      mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset
    );

    Color[] colorMap = new Color[mapWidth * mapHeight];

    for (int y = 0; y < mapHeight; y++)
    {
      for (int x = 0; x < mapWidth; x++)
      {
        float currentHeight = noiseMap[x, y];
        for (int i = 0; i < regions.Length; i++)
        {
          if (currentHeight <= regions[i].height)
          {
            colorMap[y * mapWidth + x] = regions[i].color;
            break;
          }
        }
      }
    }
    
    

    MapDisplay display = FindObjectOfType<MapDisplay>();
    if (drawMode == DrawMode.NoiseMap)
    {
      display.DrawNoiseMap(noiseMap);
      
    }
    else if (drawMode == DrawMode.ColorMap)
    {
    }

    display.DrawNoiseMap(noiseMap);
  }

  void OnValidate()
  {
    if (mapWidth < 1)
    {
      mapWidth = 1;
    }

    if (mapHeight < 1)
    {
      mapHeight = 1;
    }
    if (lacunarity < 1)
    {
      lacunarity = 1;
    }
    if (octaves < 0)
    {
      octaves = 0;
    }
  }
}

[Serializable]
public struct TerrainType
{
  public string name;
  public float height;
  public Color color;
}