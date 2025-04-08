using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector3Int> floorPositions, MapInstantiate mapInstantiate)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction3D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction3D.diagonalDirectionsList);

        foreach (var wall in basicWallPositions)
        {
            var direction = GetDirectionFromPosition(wall, floorPositions);
            mapInstantiate.PaintSingleBasicWall(wall, direction);
        }

        foreach (var corner in cornerWallPositions)
        {
            var rotation = GetCornerRotation(corner, floorPositions);
            mapInstantiate.PaintCornerWall(corner, rotation);
        }
    }

    private static HashSet<Vector3Int> FindWallsInDirections(HashSet<Vector3Int> floorPositions, List<Vector3Int> directionList)
    {
        HashSet<Vector3Int> wallPositions = new HashSet<Vector3Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (!floorPositions.Contains(neighbourPosition))
                    wallPositions.Add(neighbourPosition);
            }
        }

        return wallPositions;
    }

    private static Vector3Int GetDirectionFromPosition(Vector3Int position, HashSet<Vector3Int> floorPositions)
    {
        foreach (var direction in Direction3D.cardinalDirectionsList)
        {
            if (floorPositions.Contains(position - direction))
            {
                return direction;
            }
        }
        return Vector3Int.zero;
    }

    private static Quaternion GetCornerRotation(Vector3Int position, HashSet<Vector3Int> floorPositions)
    {
        bool hasLeft = floorPositions.Contains(position + Vector3Int.left);
        bool hasRight = floorPositions.Contains(position + Vector3Int.right);
        bool hasForward = floorPositions.Contains(position + Vector3Int.forward);
        bool hasBack = floorPositions.Contains(position + Vector3Int.back);

        if (hasLeft && hasForward)
            return Quaternion.Euler(0, 90, 0); // Quina para frente-esquerda
        if (hasRight && hasForward)
            return Quaternion.Euler(0, 0, 0);  // Quina para frente-direita
        if (hasLeft && hasBack)
            return Quaternion.Euler(0, 180, 0); // Quina para trás-esquerda
        if (hasRight && hasBack)
            return Quaternion.Euler(0, -90, 0); // Quina para trás-direita

        return Quaternion.identity; // Caso não seja identificado como quina
    }
}
