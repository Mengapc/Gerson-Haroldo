using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected MapInstantiate MapInstantiater = null;
    [SerializeField]
    protected Vector3Int startPosition = Vector3Int.zero;

    public void GenerateDungeon()
    {
        MapInstantiater.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
