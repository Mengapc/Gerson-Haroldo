using System;
using System.Collections.Generic;
using UnityEngine;

public class MapInstantiate : MonoBehaviour
{
    // Prefabs para o chão, paredes e cantos
    [SerializeField]
    private GameObject FloorPrefab, WallPrefab, CornerL, CornerU, Cantos4, ParedeDupla;

    // Prefabs para salas especiais (spawn e loja)
    [SerializeField]
    private GameObject SpawnRoomPrefab, ShopRoomPrefab;

    // Métodos para acessar os prefabs das salas especiais
    public GameObject GetSpawnRoomPrefab() => SpawnRoomPrefab;
    public GameObject GetShopRoomPrefab() => ShopRoomPrefab;

    // Dicionário que armazena os tiles (objetos) instanciados, com a posição como chave
    public Dictionary<Vector3Int, GameObject> instantiatedTiles = new Dictionary<Vector3Int, GameObject>();

    // Método para pintar os tiles do chão, chamando PaintSingleTile para cada posição
    public void PaintFloorTiles(IEnumerable<Vector3Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            PaintSingleTile(position, FloorPrefab);
        }
    }

    // Método auxiliar para pintar um único tile (piso ou qualquer outro objeto)
    private void PaintSingleTile(Vector3Int position, GameObject prefab)
    {
        // Se o tile ainda não foi instanciado naquela posição
        if (!instantiatedTiles.ContainsKey(position))
        {
            // Cria o objeto na posição (considerando o eixo y fixo em 0)
            var tilePosition = new Vector3(position.x, 0, position.z);
            var newTile = Instantiate(prefab, tilePosition, Quaternion.identity);

            // Armazena o novo tile no dicionário
            instantiatedTiles[position] = newTile;
        }
    }

    // Método para limpar todos os objetos instanciados
    public void Clear()
    {
        // Exibe o número de objetos no dicionário antes de limpar
        Debug.Log($"Número de elementos no dicionário antes da limpeza: {instantiatedTiles.Count}");

        // Destrói todos os objetos no dicionário
        foreach (var tile in instantiatedTiles.Values)
        {
            Destroy(tile);
        }

        // Limpa o dicionário
        instantiatedTiles.Clear();

        // Força a coleta de lixo, caso haja objetos pendentes para remoção
        GC.Collect();
    }


    // Método para pintar uma parede simples em uma direção específica (esquerda, direita, frente ou trás)
    public void PaintSingleBasicWall(Vector3Int position, Vector3Int direction)
{
    if (!instantiatedTiles.ContainsKey(position))
    {
        Vector3 wallOffset = Vector3.zero;

        Quaternion rotation = Quaternion.identity;
        if (direction == Vector3Int.left)
        {
            rotation = Quaternion.Euler(0, 90, 0);
            wallOffset = new Vector3(0.5f, 0f, 0f); // Ajuste para alinhar visualmente
        }
        else if (direction == Vector3Int.right)
        {
            rotation = Quaternion.Euler(0, -90, 0);
            wallOffset = new Vector3(-0.5f, 0f, 0f);
        }
        else if (direction == Vector3Int.forward)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            wallOffset = new Vector3(0f, 0f, -0.5f);
        }
        else if (direction == Vector3Int.back)
        {
            rotation = Quaternion.Euler(0, 180, 0);
            wallOffset = new Vector3(0f, 0f, 0.5f);
        }

        float wallYOffset = 0.6f; // ajuste vertical como antes
        Vector3 wallPosition = new Vector3(position.x, wallYOffset, position.z) + wallOffset;

        GameObject wall = Instantiate(WallPrefab, wallPosition, rotation);
        instantiatedTiles[position] = wall;
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
