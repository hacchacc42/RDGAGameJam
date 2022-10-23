using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;


        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, Vector2Int minSize)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> rooms = new List<BoundsInt>();

        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minSize.y && room.size.x >= minSize.x)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minSize.y*2)
                    {
                        SplitHorizontaly(room, minSize, roomsQueue);
                    }
                    else if(room.size.x >= minSize.x*2)
                    {
                        SplitVerticaly(room, minSize, roomsQueue);
                    }
                    else
                    {
                        rooms.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minSize.x * 2)
                    {
                        SplitVerticaly(room, minSize, roomsQueue);
                    }
                    else if (room.size.y >= minSize.y * 2)
                    {
                        SplitHorizontaly(room, minSize, roomsQueue);
                    }
                    else
                    {
                        rooms.Add(room);
                    }
                }
            }
        }

        return rooms;
    }

    private static void SplitVerticaly(BoundsInt room, Vector2Int minSize, Queue<BoundsInt> roomsQueue)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontaly(BoundsInt room, Vector2Int minSize, Queue<BoundsInt> roomsQueue)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x - ySplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0) //LEFT
    };
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP RIGHT
        new Vector2Int(1,-1), //DOWN RIGHT
        new Vector2Int(-1,-1), //DOWN LEFT
        new Vector2Int(-1,1) //UP LEFT
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //DOWN RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWN LEFT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(-1,1) //UP LEFT
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];
    }
}