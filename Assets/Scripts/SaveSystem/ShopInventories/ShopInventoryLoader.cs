using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShopInventoryLoader : TerraNetSaveLoader<ShopInventorySaveData>
{
    [Tooltip("Loadouts that can be loaded into")]
    [SerializeField] protected ShopInventoryDatabase loadableInventories;
    [Tooltip("Helps make sure the loadout contents are properly saved")]
    [SerializeField] protected ItemDatabase loadableItems;

    protected override bool CanLoad(ShopInventorySaveData saveData)
    {
        bool inventoryIndexNonNegative = saveData.CollectionIndex >= 0;
        bool inventoryIndexInRightRange = saveData.CollectionIndex < loadableInventories.Count();
        bool validInventoryIndex = inventoryIndexNonNegative && inventoryIndexInRightRange;

        bool inventoryIsEmpty = saveData.ContentIndexes.Count == 0;

        bool noNegativeIndex = inventoryIsEmpty || saveData.ContentIndexes.Min() >= 0;
        bool noIndexTooHigh = inventoryIsEmpty || saveData.ContentIndexes.Max() < loadableItems.Count();
        bool validContentIndexes = inventoryIsEmpty || (noNegativeIndex && noIndexTooHigh);

        return validInventoryIndex && validContentIndexes;
    }

    protected override void AlertCannotLoad(ShopInventorySaveData saveData)
    {
        string message = string.Format(loadFailMessageFormat, saveData.CollectionName);

        throw new System.InvalidOperationException(message);
    }

    protected static readonly string loadFailMessageFormat = "Cannot load Shop Inventory {0}";

    protected override void RestoreStateWith(ShopInventorySaveData saveData)
    {
        ShopInventory toLoad = loadableInventories.Contents[saveData.CollectionIndex];
        toLoad.Clear();
        AddItemsToInventory(toLoad, saveData.ContentIndexes);
    }

    protected virtual void AddItemsToInventory(ShopInventory inventory, IList<int> itemIndexes)
    {
        foreach (int index in itemIndexes)
        {
            Item item = loadableItems.Contents[index];
            inventory.Add(item);
        }
    }
}
