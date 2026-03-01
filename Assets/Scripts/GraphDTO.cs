using System;
using System.Collections.Generic;

[Serializable]
public class GraphDTO
{
    public List<BlockDTO> blocks = new List<BlockDTO>();
}

[Serializable]
public class BlockDTO
{
    public string type;
    public string assetName;
}

[Serializable]
public class SpawnDTO : BlockDTO
{
    public string primitiveType;
    public Vector3DTO position;
}

[Serializable]
public class ManipulateDTO : BlockDTO
{
    public string manipulationType;
    public string targetName;
    public Vector3DTO value;
}

[Serializable]
public class IntDTO : BlockDTO
{
    public string variableName;
    public int dataValue;
}

[Serializable]
public class ConditionalDTO : BlockDTO
{
    public string compareVariable;
    public string compareOp;
    public float compareValue;
    public List<BlockDTO> trueBlocks = new List<BlockDTO>();
    public List<BlockDTO> falseBlocks = new List<BlockDTO>();
}

// Simplify UnityEngine.Vector3 serialization
[Serializable]
public class Vector3DTO
{
    public float x;
    public float y;
    public float z;
}
