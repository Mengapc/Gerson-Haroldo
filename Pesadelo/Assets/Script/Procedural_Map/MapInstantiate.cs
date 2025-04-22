using System.Collections.Generic;
using UnityEngine;

public class MapInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject FloorPrefab, WallPrefab, CornerPrefab;

    private Dictionary<Vector3Int, GameObject> instantiatedTiles = new Dictionary<Vector3Int, GameObject>();

    public void PaintFloorTiles(IEnumerable<Vector3Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            PaintSingleTile(position, FloorPrefab);
        }
    }

    private void PaintSingleTile(Vector3Int position, GameObject prefab)
    {
        if (!instantiatedTiles.ContainsKey(position))
        {
            var tilePosition = new Vector3(position.x, 0, position.z);
            var newTile = Instantiate(prefab, tilePosition, Quaternion.identity);
            instantiatedTiles[position] = newTile;
        }
    }

    public void Clear()
    {
        // Destrói todos os objetos armazenados no dicionário
        foreach (var tile in instantiatedTiles.Values)
        {
            Destroy(tile);
        }
        instantiatedTiles.Clear();
    }

    public void PaintSingleBasicWall(Vector3Int position, Vector3Int direction)
    {
        if (!instantiatedTiles.ContainsKey(position))
        {
            var wallPosition = new Vector3(position.x, 0, position.z);
            GameObject wall = Instantiate(WallPrefab, wallPosition, Quaternion.identity);

            if (direction == Vector3Int.left)
                wall.transform.rotation = Quaternion.Euler(0, 90, 0);
            else if (direction == Vector3Int.right)
                wall.transform.rotation = Quaternion.Euler(0, -90, 0);
            else if (direction == Vector3Int.forward)
                wall.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (direction == Vector3Int.back)
                wall.transform.rotation = Quaternion.Euler(0, 180, 0);

            instantiatedTiles[position] = wall;
        }
    }

    public void PaintCornerWall(Vector3Int position, Quaternion rotation)
    {
        if (!instantiatedTiles.ContainsKey(position))
        {
            var cornerPosition = new Vector3(position.x, 0, position.z);
            GameObject corner = Instantiate(CornerPrefab, cornerPosition, rotation);
            instantiatedTiles[position] = corner;
        }
    }
}
