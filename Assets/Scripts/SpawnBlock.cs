using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/Spawn Block", fileName = "NewSpawnBlock")]
public class SpawnBlock : BlockBase
{
    public SpawnBlockData data = new SpawnBlockData();

    public override void Execute(GraphContext context)
    {
        var go = GameObject.CreatePrimitive(data.shape);
        go.transform.position = data.position;
        go.name = this.name;

        if (context.SpawnedObjects.ContainsKey(this.name))
            Debug.LogWarning($"SpawnBlock: key '{this.name}' already exists in context. Overwriting.");

        context.SpawnedObjects[this.name] = go;
    }

    public override BlockDTO Serialize()
    {
        var spawnDTO = new SpawnDTO()
        {
            type = "Spawn",
            assetName = this.name,
            primitiveType = data.shape.ToString(),
            position = new Vector3DTO { x = data.position.x, y = data.position.y, z = data.position.z }
        };

        return spawnDTO;
    }
}

[Serializable]
public class SpawnBlockData
{
    public PrimitiveType shape = PrimitiveType.Cube;
    public Vector3 position = Vector3.zero;
}
