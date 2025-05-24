using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    // ========== MÉTODOS ORIGINAIS (MANTIDOS) ========== //
    public static HashSet<Vector3Int> SimpleRandomWalk(Vector3Int startPosition, int walkLength)
    {
        HashSet<Vector3Int> path = new HashSet<Vector3Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction3D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector3Int> RandomWalkCorridor(Vector3Int startPosition, int corridorLength)
    {
        List<Vector3Int> corridor = new List<Vector3Int>();
        var direction = Direction3D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();

            if (room.size.x >= minWidth * 2 && room.size.z >= minHeight * 2)
            {
                bool splitHorizontally = Random.value < 0.5f;

                if (room.size.x < room.size.z)
                    splitHorizontally = true;
                else if (room.size.z < room.size.x)
                    splitHorizontally = false;

                if (splitHorizontally)
                {
                    SplitHorizontally(minHeight, roomsQueue, room);
                }
                else
                {
                    SplitVertically(minWidth, roomsQueue, room);
                }
            }
            else
            {
                roomsList.Add(room);
            }
        }
        return roomsList;
    }

    // ========== MÉTODOS NOVOS ADICIONADOS ========== //
    /// <summary>
    /// Cria um corredor em L direcionado para um alvo (ideal para salas especiais)
    /// </summary>
    public static List<Vector3Int> DirectedCorridor(Vector3Int startPosition, Vector3Int targetPosition, int maxLength)
    {
        List<Vector3Int> corridor = new List<Vector3Int>();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < maxLength && currentPosition != targetPosition; i++)
        {
            Vector3Int direction = Vector3Int.zero;

            // Decide se move primeiro em X ou Z baseado na maior diferença
            int xDiff = targetPosition.x - currentPosition.x;
            int zDiff = targetPosition.z - currentPosition.z;

            if (Mathf.Abs(xDiff) > Mathf.Abs(zDiff))
            {
                direction = new Vector3Int(xDiff > 0 ? 1 : -1, 0, 0);
            }
            else
            {
                direction = new Vector3Int(0, 0, zDiff > 0 ? 1 : -1);
            }

            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        // Garante que chegou exatamente na posição da porta
        if (corridor.Last() != targetPosition)
        {
            corridor.Add(targetPosition);
        }

        return corridor;
    }

    // ========== MÉTODOS AUXILIARES (MANTIDOS) ========== //
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(minWidth, room.size.x - minWidth);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, 1, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, 0, room.min.z),
                                    new Vector3Int(room.size.x - xSplit, 1, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var zSplit = Random.Range(minHeight, room.size.z - minHeight);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, 1, zSplit));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, 0, room.min.z + zSplit),
                                    new Vector3Int(room.size.x, 1, room.size.z - zSplit));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction3D
{
    public static List<Vector3Int> cardinalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1),  // Frente (Z+)
        new Vector3Int(1, 0, 0),  // Direita (X+)
        new Vector3Int(0, 0, -1), // Trás (Z-)
        new Vector3Int(-1, 0, 0), // Esquerda (X-)
    };

    public static List<Vector3Int> diagonalDirectionsList = new List<Vector3Int>
    {
        new Vector3Int(1, 0, 1),   // Frente-Direita
        new Vector3Int(1, 0, -1),  // Trás-Direita
        new Vector3Int(-1, 0, 1),  // Frente-Esquerda
        new Vector3Int(-1, 0, -1)  // Trás-Esquerda
    };

    public static Vector3Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}