using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    [SerializeField]
    public MapInstantiate mapInstantiate;


    protected override void RunProceduralGeneration()
    {
        MapInstantiater.Clear();
        HashSet<Vector3Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        MapInstantiater.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, mapInstantiate);
    }

    protected HashSet<Vector3Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector3Int position)
    {
        var currentPosition = position;
        HashSet<Vector3Int> floorPositions = new HashSet<Vector3Int>();
        for (int i = 0; i < parameters.Iterations; i++) 
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.WalkLength);
            floorPositions.UnionWith(path);
            if (parameters.StartRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
