using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class ShopInventorySaver : TerraNetDataSaver<ShopInventorySaveData, ShopInventory>, 
    ISaveCreator<ShopInventorySaveData, ShopInventory>, 
    IGroupSaver<ShopInventorySaveData>
{
    [SerializeField] protected ShopInventoryDatabase savableInventories;
    [SerializeField] protected ItemDatabase savableItems;

    public override IList<ShopInventory> ToSave
    {
        get { return savableInventories.Contents; }
    }


    public override ShopInventorySaveData CreateSave(ShopInventory inventory)
    {
        return ShopInventorySaveDataFactory.CreateFrom(inventory, savableItems, savableInventories);
    }
}
