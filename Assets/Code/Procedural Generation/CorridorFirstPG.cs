using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CorridorFirstPG : SimpleRandomWalkPG
{
    [SerializeField]
    private int corridorLength = 15;
    [SerializeField]
    private int corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercentage = .8f;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCoridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtEadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateRoomsAtEadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var pos in deadEnds)
        {
            if (roomPositions.Contains(pos) == false)
            {
                var room= RunRandomWalk(randomWalkParameters, pos);
                roomPositions.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach(var pos in floorPositions)
        {
            int neighboursCount = 0;
            foreach(var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(pos + direction))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadEnds.Add(pos);
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercentage);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCoridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);


        for (int i = 0; i < corridorCount; i++)
        {
            var corridior = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridior[corridior.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridior);
        }
    }
}
