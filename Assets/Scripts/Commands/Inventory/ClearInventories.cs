using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory", 
"Clear Inventories", 
"Removes all items from the specified inventories.")]
public class ClearInventories : Command
{
    [SerializeField] protected ShopInventoryData[] inventories = null;

    public override void OnEnter()
    {
        base.OnEnter();

        DoTheClearing();

        Continue();
    }

    protected virtual void DoTheClearing()
    {
        foreach (ShopInventory inv in inventories)
        {
            inv.Items.Clear();
        }
    }
}
