# GraphRunner

Unity tool for building and running functional graph-based logic using ScriptableObject blocks.

### Running Graph Operations

The `GraphRunner` MonoBehaviour exposes several actions via **ContextMenu**. To use them:

1. Select the `GraphRunner` GameObject in the Hierarchy.
2. Click the **three-dot menu (⋮)** on the component in the Inspector.
3. Choose the desired action from the context menu.

### Creating New Blocks

Functional blocks are implemented as **ScriptableObjects**. To create one:

1. Right-click anywhere in the **Project** window.
2. Navigate to **Create > Blocks**.
3. Select the block type you want to add.

## JSON Export / Import

All JSON export and import paths are hardcoded to the project's `Assets/` folder for simplicity
