using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeigth = 4;

    [SerializeField]
    private int dungeonWidth = 20, dungeonHeigth = 20;

    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    [SerializeField]
    private bool randomWalkRooms = false;

    [SerializeField, Range(0f, 1f)]
    private float removalChance = 0.4f; // Chance de remover salas após a geração

    [SerializeField]
    private int spacingMargin = 2; // Espaço extra entre as salas

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        // Limpa o mapa anterior antes de gerar um novo
        MapInstantiater.Clear();

        // Certificando-se de que o tamanho da dungeon está correto
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt(new Vector3Int(startPosition.x, 0, startPosition.z), new Vector3Int(dungeonWidth, 1, dungeonHeigth)),
            minRoomWidth, minRoomHeigth);

        // Aplica um filtro para remover algumas salas aleatoriamente
        roomsList = FilterRooms(roomsList, removalChance);

        // Aplica um espaçamento extra entre salas
        roomsList = ApplySpacing(roomsList, spacingMargin);

        Debug.Log($"Número de salas geradas após filtragem: {roomsList.Count}");

        HashSet<Vector3Int> floor = randomWalkRooms ? CreateRoomsRandomly(roomsList) : CreateSimpleRooms(roomsList);

        List<Vector3Int> roomCenters = new List<Vector3Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add(Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector3Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        // Pinta o chão e cria as paredes
        MapInstantiater.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, MapInstantiater);
    }

    private HashSet<Vector3Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector3Int(Mathf.RoundToInt(roomBounds.center.x), 0, Mathf.RoundToInt(roomBounds.center.z)); // Ajustado para 3D

            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter); // Agora usando randomWalkParameters corretamente

            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                    position.z >= (roomBounds.zMin + offset) && position.z <= (roomBounds.zMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector3Int> ConnectRooms(List<Vector3Int> roomCenters)
    {
        HashSet<Vector3Int> corridors = new HashSet<Vector3Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector3Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector3Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector3Int> CreateCorridor(Vector3Int currentRoomCenter, Vector3Int destination)
    {
        HashSet<Vector3Int> corridor = new HashSet<Vector3Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.z != destination.z)
        {
            position += destination.z > position.z ? Vector3Int.forward : Vector3Int.back;
            corridor.Add(position);
        }

        while (position.x != destination.x)
        {
            position += destination.x > position.x ? Vector3Int.right : Vector3Int.left;
            corridor.Add(position);
        }

        return corridor;
    }

    private Vector3Int FindClosestPointTo(Vector3Int currentRoomCenter, List<Vector3Int> roomCenters)
    {
        Vector3Int closest = Vector3Int.zero;
        float distance = float.MaxValue;

        foreach (var position in roomCenters)
        {
            float currentDistance = Vector3Int.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector3Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();

        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.z - offset; row++)
                {
                    Vector3Int position = new Vector3Int(room.min.x + col, 0, room.min.z + row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private List<BoundsInt> FilterRooms(List<BoundsInt> roomsList, float removalChance)
    {
        List<BoundsInt> filteredRooms = new List<BoundsInt>();

        foreach (var room in roomsList)
        {
            if (Random.value > removalChance)
            {
                filteredRooms.Add(room);
            }
        }
        return filteredRooms;
    }

    private List<BoundsInt> ApplySpacing(List<BoundsInt> roomsList, int spacingMargin)
    {
        List<BoundsInt> spacedRooms = new List<BoundsInt>();

        foreach (var room in roomsList)
        {
            BoundsInt newRoom = new BoundsInt(
                room.min + new Vector3Int(spacingMargin, 0, spacingMargin),
                room.size - new Vector3Int(spacingMargin * 2, 0, spacingMargin * 2)
            );

            if (newRoom.size.x > 0 && newRoom.size.z > 0)
            {
                spacedRooms.Add(newRoom);
            }
        }
        return spacedRooms;
    }
}
