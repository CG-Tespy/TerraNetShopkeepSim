using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;


public class ShopInventorySaveData : SaveData, ICollectionSOData
{
    /// <summary>
    /// Indexes of all the items in the inventory, as they are in the item database.
    /// </summary>
    [SerializeField] protected List<int> itemIndexes = new List<int>();
    [SerializeField] protected int inventoryIndex = -1;
    [SerializeField] protected string inventoryName = "";

    public virtual IList<int> ContentIndexes
    {
        get { return itemIndexes; }
        set
        {
            itemIndexes.Clear();
            itemIndexes.AddRange(value);
        }
    }

    public virtual int CollectionIndex
    {
        get { return inventoryIndex; }
        set { inventoryIndex = value; }
    }

    public virtual string CollectionName
    {
        get { return inventoryName; }
        set { inventoryName = value; }
    }

    public static ShopInventorySaveData From(ShopInventory inventory,
        ItemDatabase itemDatabase,
        ShopInventoryDatabase invDatabase)
    {
        throw new System.NotImplementedException();
    }
}
