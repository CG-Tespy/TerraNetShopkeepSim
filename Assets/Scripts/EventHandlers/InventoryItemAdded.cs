using Fungus;

[EventHandlerInfo("Shopkeep/Inventory", "Inventory Item Added", "This fires when an item has been added to certain inventories.")]
public class InventoryItemAdded : ShopInventoryContentsChanged
{
    protected override void ListenForChanges()
    {
        foreach (ShopInventory inventoryEl in inventories)
        {
            inventoryEl.ItemAdded += OnChangeHappened;
        }
    }

    protected override void UnlistenForChanges()
    {
        foreach (ShopInventory inventoryEl in inventories)
        {
            inventoryEl.ItemAdded -= OnChangeHappened;
        }
    }

}
