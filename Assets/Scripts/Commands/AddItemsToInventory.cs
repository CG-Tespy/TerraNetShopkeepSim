using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory",
                 "Add Items to Inventory",
                 @"Adds the specified items to the specified inventory.")]
[AddComponentMenu("")]
public class AddItemsToInventory : Command
{
    [SerializeField] ItemData[] items = null;
    [SerializeField] ShopInventoryData inventory;

    public override void OnEnter()
    {
        base.OnEnter();

        AddTheItems();

        Continue();
    }

    protected virtual void AddTheItems()
    {
        var invItems = inventory.Value.Items;

        for (int i = 0; i < items.Length; i++)
            invItems.Add(items[i]);
    }
}
