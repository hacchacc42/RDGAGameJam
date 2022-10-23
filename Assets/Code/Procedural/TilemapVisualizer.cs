using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private Tilemap wallTilemap;

    [SerializeField]
    private TileBase[] floorTiles;
    [SerializeField]
    private TileBase wallTile;
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        Clear();
        PaintTiles(floorPositions, floorTilemap, floorTiles[0]);
    }

    private int GetFloorTile()
    {
        int rand = Random.Range(0, 200);
        int tileChance = 1;
        for (int i = 1; i < 9; i++)
        {
            if (rand <= tileChance * i)
                return 9 - i;
        }
        return 0;
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(position, tilemap, tile, true);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile, bool floor)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        if (floor)
            tile = floorTiles[GetFloorTile()];
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(position, wallTilemap, wallTile, false);
    }
}