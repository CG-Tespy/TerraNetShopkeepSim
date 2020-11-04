using UnityEngine;
using Fungus;
using System.Collections.Generic;

[CommandInfo("Shopkeep/Inventory",
                 "Add Items to Inventory",
                 @"Adds the specified items to the specified inventory.")]
[AddComponentMenu("")]
public class AddItemsToInventory : Command
{
    [SerializeField] ItemData[] items = null;
    [Tooltip("Items in inventories here will get added to the target inventory.")]
    [SerializeField] ShopInventory[] sourceInventories = { };
    [SerializeField] ShopInventoryData targetInventory;

    public override void OnEnter()
    {
        base.OnEnter();

        AddTheItems();

        Continue();
    }

    protected virtual void AddTheItems()
    {
        IList<Item> itemsToAdd = GetItemsToAdd();

        targetInventory.Value.Items.AddRange(itemsToAdd);
    }

    protected virtual IList<Item> GetItemsToAdd()
    {
        List<Item> itemsToAdd = new List<Item>();

        for (int i = 0; i < items.Length; i++)
            itemsToAdd.Add(items[i]);

        itemsToAdd.AddRange(GetSourceInventoryItems());

        return itemsToAdd;
    }

    protected virtual IList<Item> GetSourceInventoryItems()
    {
        List<Item> sourceItems = new List<Item>();
        
        for (int i = 0; i < sourceInventories.Length; i++)
        {
            var source = sourceInventories[i];

            sourceItems.AddRange(source.Items);
        }

        return sourceItems;
    }

    
}
