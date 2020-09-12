using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopInventory", menuName ="Shopkeep/Inventory")]
public class ShopInventory : ScriptableObject
{
    [SerializeField] private Item[] startingItems = { };

    public IList<Item> Items { get; } = new List<Item>();

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
