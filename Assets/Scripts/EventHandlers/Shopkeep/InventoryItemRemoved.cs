using Fungus;

[EventHandlerInfo("Shopkeep/Inventory", "Inventory Item Removed", "This fires when an item has been removed to certain inventories.")]
public class InventoryItemRemoved : ShopInventoryContentsChanged
{
    protected override void ListenForChanges()
    {
        foreach (ShopInventory inventoryEl in inventories)
        {
            inventoryEl.ItemRemoved += OnChangeHappened;
        }
    }

    protected override void UnlistenForChanges()
    {
        foreach (ShopInventory inventoryEl in inventories)
        {
            inventoryEl.ItemRemoved -= OnChangeHappened;
        }
    }

}
