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

        // Instancia o ch�o usando o MapInstantiater
        MapInstantiater.PaintFloorTiles(floorPositions);

        // Gera paredes em torno dos corredores
        WallGenerator.CreateWalls(floorPositions, MapInstantiater);
    }

private List<Vector3Int> IncreaseCorridorSizeByOne(List<Vector3Int> corridor)
{
    List<Vector3Int> newCorridor = new List<Vector3Int>();

    for (int i = 0; i < corridor.Count; i++)
    {
        Vector3Int current = corridor[i];
        Vector3Int direction = Vector3Int.zero;

        if (i > 0)
            direction = corridor[i] - corridor[i - 1];
        else if (i < corridor.Count - 1)
            direction = corridor[i + 1] - corridor[i];

        direction = ClampToCardinal(direction);

        // Adiciona o bloco central
        newCorridor.Add(current);

        // Se estiver indo em X, adiciona blocos em Z (para dar largura)
        if (direction == Vector3Int.left || direction == Vector3Int.right)
        {
            newCorridor.Add(current + Vector3Int.forward);
            newCorridor.Add(current + Vector3Int.back);
        }
        // Se estiver indo em Z, adiciona blocos em X
        else if (direction == Vector3Int.forward || direction == Vector3Int.back)
        {
            newCorridor.Add(current + Vector3Int.right);
            newCorridor.Add(current + Vector3Int.left);
        }
        // No caso de estar parado ou num ponto de virada, adiciona cruzado
        else
        {
            newCorridor.Add(current + Vector3Int.right);
            newCorridor.Add(current + Vector3Int.left);
            newCorridor.Add(current + Vector3Int.forward);
            newCorridor.Add(current + Vector3Int.back);
        }
    }

    return newCorridor;
}

// Garante que a direção seja um dos 4 cardeais
private Vector3Int ClampToCardinal(Vector3Int direction)
{
    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        return new Vector3Int(Math.Sign(direction.x), 0, 0);
    else if (Mathf.Abs(direction.z) > 0)
        return new Vector3Int(0, 0, Math.Sign(direction.z));
    else
        return Vector3Int.zero;
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
