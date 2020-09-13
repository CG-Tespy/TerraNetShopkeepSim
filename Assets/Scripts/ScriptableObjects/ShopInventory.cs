using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopInventory", menuName ="Shopkeep/Inventory")]
public class ShopInventory : ScriptableObject
{
    [SerializeField] private Item[] startingItems = { };
    [Tooltip("What items are in the inventory at any given time.")]
    [SerializeField] private List<Item> items = new List<Item>();

    public IList<Item> Items
    {
        get { return items; }
    }
    public string Name { get { return name; } }

    protected virtual void OnEnable()
    {
        Items.Clear();
        foreach (Item item in startingItems)
        {
            if (item != null)
                Items.Add(item);
        }
    }
}
