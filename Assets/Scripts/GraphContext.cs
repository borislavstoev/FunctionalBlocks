using System.Collections.Generic;
using UnityEngine;

public class GraphContext
{
    public Dictionary<string, GameObject> SpawnedObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, int> IntVariables = new Dictionary<string, int>();

    public void Reset()
    {
        foreach (var go in SpawnedObjects.Values)
        {
            if (go != null)
                Object.DestroyImmediate(go);
        }

        SpawnedObjects.Clear();
        IntVariables.Clear();
    }
}
