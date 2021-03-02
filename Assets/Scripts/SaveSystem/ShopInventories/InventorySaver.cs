using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class InventorySaver : DataSaver<ShopInventorySaveData>, 
    ISaveCreator<ShopInventorySaveData, ShopInventory>, 
    IGroupSaver<ShopInventorySaveData>
{
    [SerializeField] List<ShopInventory> toSave = null;

    public override IList<SaveDataItem> CreateItems()
    {
        Debug.LogWarning("CreateItems for InventorySaver not implemented! Returning empty list.");
        var temp = new List<SaveDataItem>();
        return temp;
    }

    public ShopInventorySaveData CreateSave(ShopInventory from)
    {
        Debug.LogWarning("CreateSave for InventorySaver not implemented! Returning base InventoryData.");
        var temp = new ShopInventorySaveData();
        return temp;
    }

    public IList<ShopInventorySaveData> CreateSaves()
    {
        Debug.LogWarning("CreateSaves (multiple!) for InventorySaver not implemented! Returning empty list.");
        var temp = new List<ShopInventorySaveData>();
        return temp;
    }

}
