using System;
using System.Collections.Generic;
using UnityEngine;

public enum CompareOp { GreaterThan, LessThan, Equal }

[CreateAssetMenu(menuName = "Blocks/Conditional Block", fileName = "NewConditionalBlock")]
public class ConditionalBlock : BlockBase
{
    public ConditionalBlockData data = new ConditionalBlockData();

    public List<BlockBase> trueBlocks = new List<BlockBase>();

    public List<BlockBase> falseBlocks = new List<BlockBase>();

    public override void Execute(GraphContext context)
    {
        context.IntVariables.TryGetValue(data.variableName, out int current);

        bool result = data.op switch
        {
            CompareOp.GreaterThan => current > data.compareValue,
            CompareOp.LessThan => current < data.compareValue,
            CompareOp.Equal => current == data.compareValue,
            _ => false
        };

        var branch = result ? trueBlocks : falseBlocks;
        foreach (var block in branch)
        {
            if (block == null) continue;
            block.Execute(context);
        }
    }

    public override BlockDTO Serialize()
    {
        var dto = new ConditionalDTO
        {
            type = "Conditional",
            assetName = this.name,
            compareVariable = data.variableName,
            compareOp = data.op.ToString(),
            compareValue = data.compareValue
        };

        foreach (var block in trueBlocks)
        {
            if (block != null)
                dto.trueBlocks.Add(block.Serialize());
        }


        foreach (var block in falseBlocks)
        {
            if (block != null)
                dto.falseBlocks.Add(block.Serialize());
        }

        return dto;
    }
}

[Serializable]
public class ConditionalBlockData
{
    public string variableName = "";
    public CompareOp op = CompareOp.GreaterThan;
    public float compareValue = 0f;
}
