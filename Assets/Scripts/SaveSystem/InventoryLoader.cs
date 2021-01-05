using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class InventoryLoader : SaveLoader<InventoryData>
{
    public override bool Load(InventoryData saveData)
    {
        Debug.LogWarning("Load for InventoryLoader not implemented! Nothing happens.");
        return true;
    }
}
