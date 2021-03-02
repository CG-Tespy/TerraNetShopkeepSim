using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class InventoryLoader : SaveLoader<ShopInventoryData>
{
    public override bool Load(ShopInventoryData saveData)
    {
        Debug.LogWarning("Load for InventoryLoader not implemented! Nothing happens.");
        return true;
    }
}
