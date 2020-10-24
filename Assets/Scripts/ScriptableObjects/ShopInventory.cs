using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopInventory", menuName ="Shopkeep/Inventory")]
public class ShopInventory : CollectionSO<Item>
{
    /// <summary>
    /// Alias for the contents.
    /// </summary>
    public IList<Item> Items
    {
        get { return Contents; }
    }

}
