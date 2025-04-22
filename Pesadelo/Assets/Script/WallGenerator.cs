using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector3Int> floorPositions, MapInstantiate mapInstantiate)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction3D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction3D.diagonalDirectionsList);

        HashSet<Vector3Int> usedWallPositions = new HashSet<Vector3Int>();

        // Quinas internas (L) e quinas em U
        foreach (var wall in basicWallPositions)
        {
            int neighbors = 0;
            bool hasLeft = floorPositions.Contains(wall + Vector3Int.left);
            bool hasRight = floorPositions.Contains(wall + Vector3Int.right);
            bool hasForward = floorPositions.Contains(wall + Vector3Int.forward);
            bool hasBack = floorPositions.Contains(wall + Vector3Int.back);

            if (hasLeft) neighbors++;
            if (hasRight) neighbors++;
            if (hasForward) neighbors++;
            if (hasBack) neighbors++;

            // Quina em U (três lados)
            if (neighbors == 3)
            {
                var rotation = GetUCornerRotation(wall, hasLeft, hasRight, hasForward, hasBack);
                mapInstantiate.PaintCornerU(wall, rotation);
                usedWallPositions.Add(wall);
            }
            // Quina interna (L invertido)
            else if (neighbors == 2 &&
                ((hasLeft && hasBack) || (hasRight && hasBack) || (hasLeft && hasForward) || (hasRight && hasForward)))
            {
                var rotation = GetInternalCornerRotation(wall, hasLeft, hasRight, hasForward, hasBack);
                mapInstantiate.PaintInternalCornerL(wall, rotation);
                usedWallPositions.Add(wall);
            }
            // Parede dupla (corredor estreito)
            else if (hasLeft && hasRight && !hasForward && !hasBack)
            {
                mapInstantiate.PaintDoubleWall(wall, Quaternion.Euler(0, 90, 0));
                usedWallPositions.Add(wall);
            }
            else if (hasForward && hasBack && !hasLeft && !hasRight)
            {
                mapInstantiate.PaintDoubleWall(wall, Quaternion.Euler(0, 180, 0));
                usedWallPositions.Add(wall);
            }
        }

        // Paredes normais
        foreach (var wall in basicWallPositions)
        {
            if (usedWallPositions.Contains(wall)) continue;

            var direction = GetDirectionFromPosition(wall, floorPositions);
            mapInstantiate.PaintSingleBasicWall(wall, direction);
        }

        // Detectar buracos cercados por 4 pisos
        foreach (var wall in basicWallPositions)
        {
            if (floorPositions.Contains(wall))
                continue;

            bool hasLeft = floorPositions.Contains(wall + Vector3Int.left);
            bool hasRight = floorPositions.Contains(wall + Vector3Int.right);
            bool hasForward = floorPositions.Contains(wall + Vector3Int.forward);
            bool hasBack = floorPositions.Contains(wall + Vector3Int.back);

            if (hasLeft && hasRight && hasForward && hasBack)
            {
                mapInstantiate.PaintCantos4(wall);
            }
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

        if (hasLeft && hasForward) return Quaternion.Euler(0, 90, 0);
        if (hasRight && hasForward) return Quaternion.Euler(0, 0, 0);
        if (hasLeft && hasBack) return Quaternion.Euler(0, 180, 0);
        if (hasRight && hasBack) return Quaternion.Euler(0, -90, 0);

        return Quaternion.identity;
    }

    private static Quaternion GetInternalCornerRotation(Vector3Int pos, bool left, bool right, bool forward, bool back)
    {
        if (left && back) return Quaternion.Euler(0, 180, 0);       // canto traseiro-esquerdo
        if (right && back) return Quaternion.Euler(0, 90, 0);       // canto traseiro-direito
        if (left && forward) return Quaternion.Euler(0, 270, 0);    // canto frontal-esquerdo
        if (right && forward) return Quaternion.Euler(0, 0, 0);     // canto frontal-direito

        return Quaternion.identity;
    }

    private static Quaternion GetUCornerRotation(Vector3Int pos, bool left, bool right, bool forward, bool back)
    {
        if (!left) return Quaternion.Euler(0, 90, 0);      // Aberto pra esquerda
        if (!right) return Quaternion.Euler(0, -90, 0);    // Aberto pra direita
        if (!forward) return Quaternion.Euler(0, 180, 0);  // Aberto pra frente
        if (!back) return Quaternion.Euler(0, 0, 0);       // Aberto pra trás

        return Quaternion.identity;
    }

}
