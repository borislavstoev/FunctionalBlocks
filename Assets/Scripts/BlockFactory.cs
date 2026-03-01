using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public static class BlockFactory
{
    public static BlockBase FromJObject(JObject obj)
    {
        var type = obj["type"]?.Value<string>();

        switch (type)
        {
            case "Spawn":
                {
                    var dto = obj.ToObject<SpawnDTO>();
                    var block = CreateBlock<SpawnBlock>(dto.assetName);
                    block.data = new SpawnBlockData
                    {
                        shape = System.Enum.TryParse<PrimitiveType>(dto.primitiveType, out var pt)
                                   ? pt : PrimitiveType.Cube,
                        position = new Vector3(dto.position.x, dto.position.y, dto.position.z)
                    };
                    return block;
                }
            case "Manipulate":
                {
                    var dto = obj.ToObject<ManipulateDTO>();
                    var block = CreateBlock<ManipulateBlock>(dto.assetName);
                    block.data = new ManipulateBlockData
                    {
                        manipulationType = System.Enum.TryParse<ManipulationType>(dto.manipulationType, out var mt)
                                           ? mt : ManipulationType.Move,
                        targetName = dto.targetName,
                        value = new Vector3(dto.value.x, dto.value.y, dto.value.z)
                    };
                    return block;
                }
            case "SetInt":
                {
                    var dto = obj.ToObject<IntDTO>();
                    var block = CreateBlock<IntBlock>(dto.assetName);
                    block.data = new IntBlockData
                    {
                        variableName = dto.variableName,
                        value = dto.dataValue
                    };
                    return block;
                }
            case "Conditional":
                {
                    var dto = obj.ToObject<ConditionalDTO>();
                    var block = CreateBlock<ConditionalBlock>(dto.assetName);
                    block.data = new ConditionalBlockData
                    {
                        variableName = dto.compareVariable,
                        op = System.Enum.TryParse<CompareOp>(dto.compareOp, out var cop)
                                       ? cop : CompareOp.GreaterThan,
                        compareValue = dto.compareValue
                    };
                    foreach (var childObj in obj["trueBlocks"] ?? new JArray())
                        block.trueBlocks.Add(FromJObject((JObject)childObj));
                    foreach (var childObj in obj["falseBlocks"] ?? new JArray())
                        block.falseBlocks.Add(FromJObject((JObject)childObj));
                    return block;
                }
            default:
                Debug.LogWarning($"BlockFactory: unknown block type '{type}'");
                return null;
        }
    }

    private static T CreateBlock<T>(string assetName) where T : BlockBase
    {
        var block = ScriptableObject.CreateInstance<T>();
        block.name = assetName;

#if UNITY_EDITOR
        var path = $"Assets/{assetName}.asset";
        var existing = AssetDatabase.LoadAssetAtPath<T>(path);
        if (existing != null)
            AssetDatabase.DeleteAsset(path);

        AssetDatabase.CreateAsset(block, path);
#endif

        return block;
    }
}
