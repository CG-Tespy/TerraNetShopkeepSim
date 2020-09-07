using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopInventory", menuName ="Shopkeep/Inventory")]
public class ShopInventory : ScriptableObject
{
    [SerializeField] private ItemDesign[] startingItems = { };

    public IList<Item> Items { get; } = new List<Item>();

    protected virtual void OnEnable()
    {
        Items.Clear();
        foreach (ItemDesign design in startingItems)
        {
            if (design != null)
                Items.Add(Item.From(design));
        }
    }
}
