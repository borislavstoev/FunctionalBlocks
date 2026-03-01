using UnityEngine;

public abstract class BlockBase : ScriptableObject
{
    public abstract void Execute(GraphContext context);

    public abstract BlockDTO Serialize();
}
