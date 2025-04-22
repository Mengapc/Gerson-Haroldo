using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    // Tamanho mínimo que uma sala pode ter
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeigth = 4;

    // Tamanho total da dungeon
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeigth = 20;

    // Espaço vazio ao redor das salas
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    // Se as salas vão ser geradas com random walk ou não
    [SerializeField]
    private bool randomWalkRooms = false;

    // Chance de uma sala ser removida após a divisão do espaço
    [SerializeField, Range(0f, 1f)]
    private float removalChance = 0.4f;

    // Espaço extra entre as salas (evita que fiquem coladas)
    [SerializeField]
    private int spacingMargin = 2;

    // Método principal de geração procedural chamado pela base
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

        // ===== SALAS ESPECIAIS =====
        List<BoundsInt> specialRooms = new List<BoundsInt>();
        List<Vector3Int> roomCenters = new List<Vector3Int>();

        // Seleciona aleatoriamente duas salas para serem especiais
        var spawnRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(spawnRoom);

        var shopRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(shopRoom);

        specialRooms.Add(spawnRoom);
        specialRooms.Add(shopRoom);

        // Instancia as salas especiais no centro da área
        Vector3Int spawnCenter = Vector3Int.RoundToInt(spawnRoom.center);
        Vector3Int shopCenter = Vector3Int.RoundToInt(shopRoom.center);

        GameObject spawnPrefab = MapInstantiater.GetSpawnRoomPrefab();
        GameObject shopPrefab = MapInstantiater.GetShopRoomPrefab();

        var spawnInstance = Instantiate(spawnPrefab, new Vector3(spawnCenter.x, 0, spawnCenter.z), Quaternion.identity);
        var shopInstance = Instantiate(shopPrefab, new Vector3(shopCenter.x, 0, shopCenter.z), Quaternion.identity);

        mapInstantiate.instantiatedTiles[spawnCenter] = spawnInstance;
        mapInstantiate.instantiatedTiles[shopCenter] = shopInstance;

        // Salva os centros para conectar depois
        roomCenters.Add(spawnCenter);
        roomCenters.Add(shopCenter);

        // ============================

        // Remove algumas salas (mas já removemos as especiais antes)
        roomsList = FilterRooms(roomsList, removalChance);

        // Aplica espaçamento extra
        roomsList = ApplySpacing(roomsList, spacingMargin);

        Debug.Log($"Número de salas normais após filtragem: {roomsList.Count}");

        HashSet<Vector3Int> floor = randomWalkRooms ? CreateRoomsRandomly(roomsList) : CreateSimpleRooms(roomsList);

        // Adiciona os centros das salas comuns
        foreach (var room in roomsList)
        {
            roomCenters.Add(Vector3Int.RoundToInt(room.center));
        }

        // Conecta todas as salas (especiais + normais)
        HashSet<Vector3Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        MapInstantiater.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, MapInstantiater);
    }

    // Gera salas com random walk dentro dos limites da sala original
    private HashSet<Vector3Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector3Int(Mathf.RoundToInt(roomBounds.center.x), 0, Mathf.RoundToInt(roomBounds.center.z));

            // Caminhada aleatória para criar um formato irregular de sala
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            foreach (var position in roomFloor)
            {
                // Garante que o chão gerado fique dentro dos limites da sala (com margem)
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                    position.z >= (roomBounds.zMin + offset) && position.z <= (roomBounds.zMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    // Conecta os centros das salas com corredores simples em L
    private HashSet<Vector3Int> ConnectRooms(List<Vector3Int> roomCenters)
    {
        HashSet<Vector3Int> corridors = new HashSet<Vector3Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            // Encontra a sala mais próxima
            Vector3Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);

            // Cria um corredor entre as duas salas
            HashSet<Vector3Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

            // Atualiza o ponto de referência
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    // Cria um corredor em L ligando dois pontos
    private HashSet<Vector3Int> CreateCorridor(Vector3Int currentRoomCenter, Vector3Int destination)
    {
        HashSet<Vector3Int> corridor = new HashSet<Vector3Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        // Primeiro anda na vertical (eixo Z)
        while (position.z != destination.z)
        {
            position += destination.z > position.z ? Vector3Int.forward : Vector3Int.back;
            corridor.Add(position);
        }

        // Depois anda na horizontal (eixo X)
        while (position.x != destination.x)
        {
            position += destination.x > position.x ? Vector3Int.right : Vector3Int.left;
            corridor.Add(position);
        }

        return corridor;
    }

    // Acha o centro de sala mais próximo
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

    // Cria salas simples, preenchendo o retângulo interno com piso
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

    // Remove salas com base em uma chance aleatória
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

    // Diminui o tamanho das salas pra deixar espaço entre elas
    private List<BoundsInt> ApplySpacing(List<BoundsInt> roomsList, int spacingMargin)
    {
        List<BoundsInt> spacedRooms = new List<BoundsInt>();

        foreach (var room in roomsList)
        {
            BoundsInt newRoom = new BoundsInt(
                room.min + new Vector3Int(spacingMargin, 0, spacingMargin),
                room.size - new Vector3Int(spacingMargin * 2, 0, spacingMargin * 2)
            );

            // Só adiciona se ainda tiver tamanho positivo
            if (newRoom.size.x > 0 && newRoom.size.z > 0)
            {
                spacedRooms.Add(newRoom);
            }
        }
        return spacedRooms;
    }
}
