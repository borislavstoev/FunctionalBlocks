using System;
using UnityEngine;

public enum ManipulationType { Move, Rotate, Scale }

[CreateAssetMenu(menuName = "Blocks/Manipulate Block", fileName = "NewManipulateBlock")]
public class ManipulateBlock : BlockBase
{
    public ManipulateBlockData data = new ManipulateBlockData();

    public override void Execute(GraphContext context)
    {
        if (!context.SpawnedObjects.TryGetValue(data.targetName, out var go) || go == null)
        {
            Debug.LogWarning($"ManipulateBlock: no object found for key '{data.targetName}'");
            return;
        }

        switch (data.manipulationType)
        {
            case ManipulationType.Move:
                go.transform.position += data.value;
                break;
            case ManipulationType.Rotate:
                go.transform.eulerAngles += data.value;
                break;
            case ManipulationType.Scale:
                go.transform.localScale += data.value;
                break;
        }
    }

    public override BlockDTO Serialize()
    {
        var manipulateDTO = new ManipulateDTO
        {
            type = "Manipulate",
            assetName = this.name,
            manipulationType = data.manipulationType.ToString(),
            targetName = data.targetName,
            value = new Vector3DTO { x = data.value.x, y = data.value.y, z = data.value.z }
        };

        return manipulateDTO;
    }
}

[Serializable]
public class ManipulateBlockData
{
    public ManipulationType manipulationType = ManipulationType.Move;
    public string targetName = "";
    public Vector3 value = Vector3.zero;
}
