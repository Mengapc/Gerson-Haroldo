using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeigth = 4;

    [SerializeField] private int dungeonWidth = 20, dungeonHeigth = 20;

    [SerializeField][Range(0, 10)] private int offset = 1;

    [SerializeField] private bool randomWalkRooms = false;

    [SerializeField, Range(0f, 1f)] private float removalChance = 0.4f;

    [SerializeField] private int spacingMargin = 2;

    [SerializeField] private int corridorWidth = 3; // largura dos corredores

    [SerializeField] private bool useCustomSeed = false;

    [SerializeField] private int customSeed = 0;

    [SerializeField] private GameObject enemyPrefab; //spawn inimigo

    public PlayerMovement playerMovementScript;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Start()
    {
        CreateRooms();
    }

    // Salva a seed
    private void SaveSeed(int seed)
    {
        PlayerPrefs.SetInt("CustomSeed", seed); // Salva a seed nas preferencias do player
        PlayerPrefs.Save(); // Garante que a seed seja salva
    }

    // Carrega a seed salva, ou gera uma nova seed aleatória
    private int LoadSeed()
    {
        if (PlayerPrefs.HasKey("CustomSeed"))
        {
            return PlayerPrefs.GetInt("CustomSeed"); // Carrega a seed salva
        }
        return Random.Range(1, 1000000); // Se não houver seed salva, gera uma nova
    }


    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    //código polak spawnar inimigos

    private void SpawnEnemies(HashSet<Vector3Int> floorTiles, HashSet<Vector3Int> corridorTiles)
    {
        foreach (var tilePos in floorTiles)
        {
            // Pular se for corredor
            if (corridorTiles.Contains(tilePos))
                continue;

            // 1 em 20 chance
            if (Random.Range(0, 20) == 0)
            {
                Vector3 spawnPosition = new Vector3(tilePos.x, 10f, tilePos.z); // altura ajustável
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    //fim do código polak spawnar inimigos
    private void CreateRooms()
    {
        // Usar a seed salva ou gerar uma nova
        if (useCustomSeed)
        {
            customSeed = LoadSeed(); // Carrega a seed salva
            SaveSeed(customSeed); // Salva a seed dnv
            Random.InitState(customSeed);
        }
        else
        {
            customSeed = Random.Range(1, 1000000); // Gera uma nova seed
            SaveSeed(customSeed); // Salva essa seed nas preferencias do player
            Random.InitState(customSeed);
            Debug.Log("Seed Do Mapa: " + customSeed);
        }

        MapInstantiater.Clear();

        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt(new Vector3Int(startPosition.x, 0, startPosition.z),
            new Vector3Int(dungeonWidth, 1, dungeonHeigth)), minRoomWidth, minRoomHeigth);

        List<BoundsInt> specialRooms = new List<BoundsInt>();
        List<Vector3Int> roomCenters = new List<Vector3Int>();

        // Escolhe salas especiais
        var spawnRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(spawnRoom);

        var shopRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(shopRoom);

        var altarRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(altarRoom);

        specialRooms.Add(spawnRoom);
        specialRooms.Add(shopRoom);
        specialRooms.Add(altarRoom);

        // Instancia os prefabs nas posições das salas
        GameObject spawnPrefab = MapInstantiater.GetSpawnRoomPrefab();
        GameObject shopPrefab = MapInstantiater.GetShopRoomPrefab();
        GameObject altarPrefab = MapInstantiater.GetAltarRoomPrefab();

        var spawnInstance = Instantiate(spawnPrefab, new Vector3(spawnRoom.center.x, 0, spawnRoom.center.z), Quaternion.identity);
        var shopInstance = Instantiate(shopPrefab, new Vector3(shopRoom.center.x, 0, shopRoom.center.z), Quaternion.identity);
        var altarInstance = Instantiate(altarPrefab, new Vector3(altarRoom.center.x, 0, altarRoom.center.z), Quaternion.identity);

        // Armazena as instâncias no dicionário
        mapInstantiate.instantiatedTiles[Vector3Int.RoundToInt(spawnRoom.center)] = spawnInstance;
        mapInstantiate.instantiatedTiles[Vector3Int.RoundToInt(shopRoom.center)] = shopInstance;
        mapInstantiate.instantiatedTiles[Vector3Int.RoundToInt(altarRoom.center)] = altarInstance;

        // Busca os pontos de entrada nos prefabs instanciados
        Transform spawnEntranceA = spawnInstance.transform.Find("PortaS1");
        Transform spawnEntranceB = spawnInstance.transform.Find("PortaS2");

        Transform shopEntranceA = shopInstance.transform.Find("PortaL1");
        Transform shopEntranceB = shopInstance.transform.Find("PortaL2");

        Transform altarEntranceA = altarInstance.transform.Find("PortaA1");
        Transform altarEntranceB = altarInstance.transform.Find("PortaA2");

        // Armazena as posições dos pontos de entrada
        roomCenters.Add(Vector3Int.RoundToInt(spawnEntranceA.position));
        roomCenters.Add(Vector3Int.RoundToInt(spawnEntranceB.position));

        roomCenters.Add(Vector3Int.RoundToInt(shopEntranceA.position));
        roomCenters.Add(Vector3Int.RoundToInt(shopEntranceB.position));

        roomCenters.Add(Vector3Int.RoundToInt(altarEntranceA.position));
        roomCenters.Add(Vector3Int.RoundToInt(altarEntranceB.position));


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

        SpawnEnemies(floor, corridors); //chama o spawn de inimigos

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

        return CorridorUtils.IncreaseCorridorSize(corridor, corridorWidth);
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
        yield return new WaitForSeconds(0.2f);

        var playerMovementScript = player.GetComponent<PlayerMovement>();
        if (playerMovementScript != null)
        {
            playerMovementScript.Spawn();
        }
        else
        {
            Debug.LogError("PlayerMovement script não encontrado no jogador!");
        }

        var boundaryManager = player.GetComponent<PlayerBoundaryManager>();
        if (boundaryManager != null)
        {
            boundaryManager.InitializeBoundary();
        }
        else
        {
            Debug.LogWarning("PlayerBoundaryManager não encontrado.");
        }
    }
}