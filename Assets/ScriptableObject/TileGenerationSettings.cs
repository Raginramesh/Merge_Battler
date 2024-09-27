using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GenerationSettings")]
public class TileGenerationSettings : ScriptableObject
{
    public enum HexTileType
    {
        PlainHex,
        WaterHex,
        MountainHex
    }
    
    public enum TileType
    {
        Plain,
        Water,
        Mountain
    }
    
    public GameObject plainHex;
    public GameObject waterHex;
    public GameObject mountainHex;
    
    public GameObject plain;
    public GameObject water;
    public GameObject mountain;

    public GameObject GetHexTile(HexTileType tileType)
    {
        switch (tileType)
        {
            case HexTileType.PlainHex:
                return plainHex;
            case HexTileType.WaterHex:
                return waterHex;
            case HexTileType.MountainHex:
                return mountainHex;
        }
        return null;
    }
    
    
    public GameObject GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Plain:
                return plain;
            case TileType.Water:
                return water;
            case TileType.Mountain:
                return mountain;
        }
        return null;
    }
}
