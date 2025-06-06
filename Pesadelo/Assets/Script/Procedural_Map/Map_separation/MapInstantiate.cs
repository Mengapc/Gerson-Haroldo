using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapInstantiate : MonoBehaviour
{
    // Prefabs para o ch�o, paredes e cantos
    [SerializeField]
    private GameObject FloorPrefab, WallPrefab, CornerL, CornerU, Cantos4, ParedeDupla, LightPrefab;

    // Prefabs para salas especiais (spawn e loja)
    [SerializeField]
    private GameObject SpawnRoomPrefab, ShopRoomPrefab, AltarRoomPrefab, PortalRoomPrefab;

    // M�todos para acessar os prefabs das salas especiais
    public GameObject GetSpawnRoomPrefab() => SpawnRoomPrefab;
    public GameObject GetShopRoomPrefab() => ShopRoomPrefab;
    public GameObject GetAltarRoomPrefab() => AltarRoomPrefab;
    public GameObject GetPortalRoomPrefab() => PortalRoomPrefab;

    // Dicion�rio que armazena os tiles (objetos) instanciados, com a posi��o como chave
    public Dictionary<Vector3Int, GameObject> instantiatedTiles = new Dictionary<Vector3Int, GameObject>();

    // M�todo para pintar os tiles do ch�o, chamando PaintSingleTile para cada posi��o
    public void PaintFloorTiles(IEnumerable<Vector3Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            PaintSingleTile(position, FloorPrefab);
        }
    }

    // M�todo auxiliar para pintar um �nico tile (piso ou qualquer outro objeto)
    private void PaintSingleTile(Vector3Int position, GameObject prefab)
    {
        // Se o tile ainda n�o foi instanciado naquela posi��o
        if (!instantiatedTiles.ContainsKey(position))
        {
            // Cria o objeto na posi��o (considerando o eixo y fixo em 0)
            var tilePosition = new Vector3(position.x, 0, position.z);
            var newTile = Instantiate(prefab, tilePosition, Quaternion.identity);

            // Armazena o novo tile no dicion�rio
            instantiatedTiles[position] = newTile;
        }
    }

    // M�todo para limpar todos os objetos instanciados
    public void Clear()
    {
        Debug.Log($"Número de elementos no dicionário antes da limpeza: {instantiatedTiles.Count}");

        foreach (var tile in instantiatedTiles.Values)
        {
            Destroy(tile);
        }

        instantiatedTiles.Clear();
        GC.Collect();

        var inimigos = GameObject.FindGameObjectsWithTag("basic_enemy");

        foreach (var inimigo in inimigos)
        {
            Destroy(inimigo);
        }

        var leftovers = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None)
            .Where(go => go.name.Contains("Parede(Clone)")).ToList();

        foreach (var go in leftovers)
        {
            Destroy(go);
        }
    }


    // M�todo para pintar uma parede simples em uma dire��o espec�fica (esquerda, direita, frente ou tr�s)
    public void PaintSingleBasicWall(Vector3Int position, Vector3Int direction)
    {
        if (!instantiatedTiles.ContainsKey(position))
        {
            Vector3 wallOffset = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            if (direction == Vector3Int.left)
            {
                rotation = Quaternion.Euler(0, 90, 0);
                wallOffset = new Vector3(0.5f, 0f, 0f);
            }
            else if (direction == Vector3Int.right)
            {
                rotation = Quaternion.Euler(0, -90, 0);
                wallOffset = new Vector3(-0.5f, 0f, 0f);
            }
            else if (direction == Vector3Int.forward)
            {
                rotation = Quaternion.Euler(0, 180, 0);
                wallOffset = new Vector3(0f, 0f, -0.5f);
            }
            else if (direction == Vector3Int.back)
            {
                rotation = Quaternion.Euler(0, 0, 0);
                wallOffset = new Vector3(0f, 0f, 0.5f);
            }

            float wallYOffset = 0.6f;
            Vector3 wallPosition = new Vector3(position.x, wallYOffset, position.z) + wallOffset;

            GameObject wall = Instantiate(WallPrefab, wallPosition, rotation);
            instantiatedTiles[position] = wall;

            // CHANCE DE ADICIONAR LUZ
            float chance = UnityEngine.Random.value;
            if (chance <= 0.2f)
            {
                // Tenta encontrar o filho "Luz" dentro da parede
                Transform lightAnchor = wall.transform.Find("Luz");
                if (lightAnchor != null)
                {
                    Instantiate(LightPrefab, lightAnchor.position, lightAnchor.rotation, lightAnchor);
                }
                else
                {
                    Debug.LogWarning($"Objeto 'Luz' não encontrado na parede instanciada em {wallPosition}");
                }
            }
        }
    }

    public void PaintInternalCornerL(Vector3Int position, Quaternion rotation)
    {
        var cornerPosition = new Vector3(position.x, 0.6f, position.z);
        GameObject corner = Instantiate(CornerL, cornerPosition, rotation); // usa prefab de quina interna
        instantiatedTiles[position] = corner;
    }

    public void PaintCornerU(Vector3Int position, Quaternion rotation)
    {
        var cornerPosition = new Vector3(position.x, 0.6f, position.z);
        GameObject corner = Instantiate(CornerU, cornerPosition, rotation);
        instantiatedTiles[position] = corner;
    }
    public void PaintCantos4(Vector3Int position)
    {
        var pos = new Vector3(position.x, 0.6f, position.z); // mesma altura das paredes
        GameObject tile = Instantiate(Cantos4, pos, Quaternion.identity);
        instantiatedTiles[position] = tile;
    }
    public void PaintDoubleWall(Vector3Int position, Quaternion rotation)
    {
        if (!instantiatedTiles.ContainsKey(position))
        {
            Vector3 wallPosition = new Vector3(position.x, 0.6f, position.z); // mesma altura das paredes
            GameObject doubleWall = Instantiate(ParedeDupla, wallPosition, rotation);
            instantiatedTiles[position] = doubleWall;
        }
    }
}
