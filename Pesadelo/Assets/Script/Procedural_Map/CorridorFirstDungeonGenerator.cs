using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        // Limpa a dungeon anterior
        MapInstantiater.Clear();

        HashSet<Vector3Int> floorPositions = new HashSet<Vector3Int>();
        HashSet<Vector3Int> potentialRoomPositions = new HashSet<Vector3Int>();

        List<List<Vector3Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector3Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector3Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            floorPositions.UnionWith(corridors[i]);

        }

        // Instancia o chão usando o MapInstantiater
        MapInstantiater.PaintFloorTiles(floorPositions);

        // Gera paredes em torno dos corredores
        WallGenerator.CreateWalls(floorPositions, MapInstantiater);
    }

    private List<Vector3Int> IncreaseCorridorSizeByOne(List<Vector3Int> corridor)
    {
        List<Vector3Int> newCorridor = new List<Vector3Int>();
        Vector3Int previousDirection = Vector3Int.zero;

        for (int i = 0; i < corridor.Count; i++)
        {
            if (i == 0) // Se for o primeiro elemento, simplesmente adiciona ele e pula a lógica de direção
            {
                newCorridor.Add(corridor[i]);
                continue;
            }

            Vector3Int directionForCell = corridor[i] - corridor[i - 1];

            if (previousDirection != Vector3Int.zero && directionForCell != previousDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector3Int(x, 0, y)); // Corrigido para 3D (mantendo y = 0)
                    }
                }
                previousDirection = directionForCell;
            }
            else
            {
                Vector3Int newCorridorTileOffset = GetDirection90From(directionForCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
                previousDirection = directionForCell;
            }
        }
        return newCorridor;
    }


    private Vector3Int GetDirection90From(Vector3Int direction)
    {
        if (direction == Vector3Int.forward)
            return Vector3Int.right;
        if (direction == Vector3Int.right)
            return Vector3Int.back;
        if (direction == Vector3Int.back)
            return Vector3Int.left;
        if (direction == Vector3Int.left)
            return Vector3Int.forward;
        return Vector3Int.zero;
    }

    private void CreateRoomsAtDeadEnd(List<Vector3Int> deadEnds, HashSet<Vector3Int> roomFloors)
    {
        foreach(var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector3Int> FindAllDeadEnds(HashSet<Vector3Int> floorPositions)
    {
        List<Vector3Int> deadEnds = new List<Vector3Int>();
        foreach (Vector3Int position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction3D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            if(neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }
    private HashSet<Vector3Int> CreateRooms(HashSet<Vector3Int> potentialRoomPositions)
    {
        HashSet<Vector3Int> roomPositions = new HashSet<Vector3Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector3Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector3Int>>  CreateCorridors(HashSet<Vector3Int> floorPositions, HashSet<Vector3Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector3Int>> corridors = new List<List<Vector3Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }
}
