using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
                 "Add Item to Inventory",
                 @"Adds the specified item to the specified inventory.")]
[AddComponentMenu("")]
public class AddItemToInventory : Command
{
    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] ItemVariable item = null;
    [SerializeField] ShopInventoryData inventory;

    public override void OnEnter()
    {
        base.OnEnter();
        var invItems = inventory.Value.Items;
        invItems.Add(item.Value);
        Continue();
    }
}
