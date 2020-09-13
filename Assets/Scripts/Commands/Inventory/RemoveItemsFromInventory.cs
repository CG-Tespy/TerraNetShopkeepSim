using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory",
                 "Remove Items from Inventory",
                 @"Removes the specified item from the specified inventory")]
[AddComponentMenu("")]
public class RemoveItemsFromInventory : Command
{
    [SerializeField] ItemData[] items = null;
    [SerializeField] ShopInventoryData inventory;

    public override void OnEnter()
    {
        base.OnEnter();
        RemoveItems();
        Continue();
    }

    protected virtual void RemoveItems()
    {
        var invItems = inventory.Value.Items;

        foreach (Item itemEl in items)
        {
            invItems.Remove(itemEl);
        }
    }
}
