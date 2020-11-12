using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Add Items to Collection",
    "Adds items and/or the contents of ShopInventories to an Object Collection.")]
public class AddItemsToCollection : Command
{
    [SerializeField] protected ItemData[] items;
    [SerializeField] protected ShopInventoryData[] inventories;
    [SerializeField] protected CollectionData collection;

    protected ObjectCollection ObjectCollection
    {
        get { return collection.Value as ObjectCollection; }
    }

    public override void OnEnter()
    {
        base.OnEnter();

        AddIndividualItems();
        AddInventoryItems();

        Continue();
    }

    protected virtual void AddIndividualItems()
    {
        foreach (Item itemEl in items)
            ObjectCollection.Add(itemEl);
    }

    protected virtual void AddInventoryItems()
    {
        foreach (ShopInventory inventoryEl in inventories)
            foreach (Item itemEl in inventoryEl.Items)
                ObjectCollection.Add(itemEl);
    }
}
