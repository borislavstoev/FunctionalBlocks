using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/Int Block", fileName = "NewIntBlock")]
public class IntBlock : BlockBase
{
    public IntBlockData data = new IntBlockData();

    public override void Execute(GraphContext context)
    {
        if (string.IsNullOrEmpty(data.variableName))
        {
            Debug.LogWarning("DataBlock: variableName is empty.");
            return;
        }

        context.IntVariables[data.variableName] = data.value;
    }

    public override BlockDTO Serialize()
    {
        var intDTO = new IntDTO()
        {
            type = "SetInt",
            assetName = this.name,
            variableName = data.variableName,
            dataValue = data.value
        };

        return intDTO;
    }
}

[Serializable]
public class IntBlockData
{
    public string variableName = "";
    public int value = 0;
}
