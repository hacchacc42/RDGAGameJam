using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class RoomFristPG : SimpleRandomWalkPG
{
    [SerializeField]
    Vector2Int minRoomSize = new Vector2Int(4, 4);
    [SerializeField]
    Vector2Int dungeonSize = new Vector2Int(20, 20);
    [SerializeField]
    [Range(0,10)]
    int offset = 1;
    [SerializeField]
    bool randomWalkRooms = false;


    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var rooms = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonSize.x, dungeonSize.y, 0)),
            minRoomSize);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(rooms);
        }
        else
        {
            floor = CreateSimpleRooms(rooms);
        }
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in rooms)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

        var currentRoomCenter = roomCenters[Random.Range(0,roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach(var roomPos in roomCenters)
        {
            float currenDist = Vector2.Distance(currentRoomCenter, roomPos);
            if(currenDist<distance)
            {
                distance= currenDist;
                closest = roomPos;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> rooms)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in rooms)
        {
            var roomBounds = room;
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach(var pos in roomFloor)
            {
                if (pos.x >= (roomBounds.xMin + offset) && pos.x <= (roomBounds.xMax - offset) &&
                   pos.y >= (roomBounds.yMin + offset) && pos.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(pos);
                }
            }
        }

        return floor;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> rooms)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach(var room in rooms)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for(int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                }
            }
        }

        return floor;
    }
}
