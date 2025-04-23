using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeigth = 4;
    [SerializeField] private int dungeonWidth = 20, dungeonHeigth = 20;
    [SerializeField] [Range(0, 10)] private int offset = 1;
    [SerializeField] private bool randomWalkRooms = false;
    [SerializeField, Range(0f, 1f)] private float removalChance = 0.4f;
    [SerializeField] private int spacingMargin = 2;
    [SerializeField] private int corridorWidth = 3; // ✅ largura dos corredores

    public PlayerMovement playerMovementScript;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        MapInstantiater.Clear();

        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt(new Vector3Int(startPosition.x, 0, startPosition.z),
            new Vector3Int(dungeonWidth, 1, dungeonHeigth)), minRoomWidth, minRoomHeigth);

        List<BoundsInt> specialRooms = new List<BoundsInt>();
        List<Vector3Int> roomCenters = new List<Vector3Int>();

        var spawnRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(spawnRoom);

        var shopRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(shopRoom);

        specialRooms.Add(spawnRoom);
        specialRooms.Add(shopRoom);

        Vector3Int spawnCenter = Vector3Int.RoundToInt(spawnRoom.center);
        Vector3Int shopCenter = Vector3Int.RoundToInt(shopRoom.center);

        GameObject spawnPrefab = MapInstantiater.GetSpawnRoomPrefab();
        GameObject shopPrefab = MapInstantiater.GetShopRoomPrefab();

        var spawnInstance = Instantiate(spawnPrefab, new Vector3(spawnCenter.x, 0, spawnCenter.z), Quaternion.identity);
        var shopInstance = Instantiate(shopPrefab, new Vector3(shopCenter.x, 0, shopCenter.z), Quaternion.identity);

        mapInstantiate.instantiatedTiles[spawnCenter] = spawnInstance;
        mapInstantiate.instantiatedTiles[shopCenter] = shopInstance;

        roomCenters.Add(spawnCenter);
        roomCenters.Add(shopCenter);

        roomsList = FilterRooms(roomsList, removalChance);
        roomsList = ApplySpacing(roomsList, spacingMargin);

        Debug.Log($"Número de salas normais após filtragem: {roomsList.Count}");

        HashSet<Vector3Int> floor = randomWalkRooms ? CreateRoomsRandomly(roomsList) : CreateSimpleRooms(roomsList);

        foreach (var room in roomsList)
        {
            roomCenters.Add(Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector3Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        MapInstantiater.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, MapInstantiater);

        StartCoroutine(DelayedSpawn());
    }

    private HashSet<Vector3Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        foreach (var roomBounds in roomsList)
        {
            var roomCenter = new Vector3Int(Mathf.RoundToInt(roomBounds.center.x), 0, Mathf.RoundToInt(roomBounds.center.z));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

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
            corridors.UnionWith(newCorridor);

            currentRoomCenter = closest;
        }

        return corridors;
    }

    private HashSet<Vector3Int> CreateCorridor(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> corridor = new List<Vector3Int>();
        Vector3Int position = start;
        corridor.Add(position);

        while (position.z != end.z)
        {
            position += end.z > position.z ? Vector3Int.forward : Vector3Int.back;
            corridor.Add(position);
        }

        while (position.x != end.x)
        {
            position += end.x > position.x ? Vector3Int.right : Vector3Int.left;
            corridor.Add(position);
        }

        return CorridorUtils.IncreaseCorridorSize(corridor, corridorWidth); // ✅ largura definida pela variável
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

    private IEnumerator DelayedSpawn()
    {
        yield return null;

        if (playerMovementScript != null)
        {
            playerMovementScript.Spawn();
        }
        else
        {
            Debug.LogError("playerMovementScript não está atribuído.");
        }
    }
}
