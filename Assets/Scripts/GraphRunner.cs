using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GraphRunner : MonoBehaviour
{
    [SerializeField] public List<BlockBase> blocks = new List<BlockBase>();

    private GraphContext _context = new GraphContext();
    private const string JSON_FILE_NAME = "graph_export.json";

    [ContextMenu("Run Graph")]
    public void RunGraph()
    {
        _context.Reset();

        foreach (var block in blocks)
        {
            if (block == null)
            {
                Debug.LogWarning("GraphRunner: null block encountered, skipping.");
                continue;
            }
            block.Execute(_context);
        }
    }

    [ContextMenu("Export Graph to JSON")]
    public void ExportGraph()
    {
        var dto = new GraphDTO();
        foreach (var block in blocks)
            dto.blocks.Add(block.Serialize());

        var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        var path = Path.Combine(Application.dataPath, JSON_FILE_NAME);
        File.WriteAllText(path, json);
        Debug.Log($"Graph exported to: {path}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    [ContextMenu("Import Graph from JSON")]
    public void ImportGraph()
    {
        // Hardcoding the expected json file path for simplicity. Should be selected with a file picker instead.
        var path = Path.Combine(Application.dataPath, JSON_FILE_NAME);
        if (!File.Exists(path))
        {
            Debug.LogError($"Import failed: no file at {path}");
            return;
        }

        var root = JObject.Parse(File.ReadAllText(path));

        blocks.Clear();
        foreach (var blockObj in root["blocks"])
        {
            var block = BlockFactory.FromJObject((JObject)blockObj);
            if (block != null) 
                blocks.Add(block);
        }

#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif

        Debug.Log($"Graph imported: {blocks.Count} blocks.");
    }
}
