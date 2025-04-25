using System.Collections.Generic;
using UnityEngine;

public static class CorridorUtils
{
    // Largura personalizável
    public static HashSet<Vector3Int> IncreaseCorridorSize(List<Vector3Int> corridor, int width)
    {
        HashSet<Vector3Int> widenedCorridor = new HashSet<Vector3Int>();

        foreach (var position in corridor)
        {
            for (int x = -width / 2; x <= width / 2; x++)
            {
                for (int z = -width / 2; z <= width / 2; z++)
                {
                    Vector3Int newPos = new Vector3Int(position.x + x, position.y, position.z + z);
                    widenedCorridor.Add(newPos);
                }
            }
        }

        return widenedCorridor;
    }

    // Versão antiga (largura 3)
    public static HashSet<Vector3Int> IncreaseCorridorSizeByOne(List<Vector3Int> corridor)
    {
        return IncreaseCorridorSize(corridor, 3);
    }
}
