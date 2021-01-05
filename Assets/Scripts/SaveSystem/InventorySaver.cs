using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class InventorySaver : DataSaver<InventoryData>, ISaveCreator<InventoryData, ShopInventory>, IGroupSaver<InventoryData>
{
    [SerializeField] List<ShopInventory> toSave = null;

    public override IList<SaveDataItem> CreateItems()
    {
        Debug.LogWarning("CreateItems for InventorySaver not implemented! Returning empty list.");
        var temp = new List<SaveDataItem>();
        return temp;
    }

    public InventoryData CreateSave(ShopInventory from)
    {
        Debug.LogWarning("CreateSave for InventorySaver not implemented! Returning base InventoryData.");
        var temp = new InventoryData();
        return temp;
    }

    public IList<InventoryData> CreateSaves()
    {
        Debug.LogWarning("CreateSaves (multiple!) for InventorySaver not implemented! Returning empty list.");
        var temp = new List<InventoryData>();
        return temp;
    }

}
