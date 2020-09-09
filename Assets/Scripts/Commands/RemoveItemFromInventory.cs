using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep",
                 "Remove Item from Inventory",
                 @"Removes the specified item from the specified inventory")]
[AddComponentMenu("")]
public class RemoveItemFromInventory : Command
{
    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] ItemVariable item = null;
    [SerializeField] ShopInventoryData inventory;

    public override void OnEnter()
    {
        base.OnEnter();
        var invItems = inventory.Value.Items;
        invItems.Remove(item.Value);
        Continue();
    }
}
