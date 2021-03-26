using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This helps keep track of the game's ShopInventories, helping the save system
/// identify which are which when doing its thing.
/// </summary>
[CreateAssetMenu(fileName = "NewShopInventoryDatabase", menuName = "Shopkeep/ShopInventoryDatabase")]
public class ShopInventoryDatabase : CollectionSO<ShopInventory>
{
    /// <summary>
    /// Alias for the Contents
    /// </summary>
    public virtual IList<ShopInventory> Shops { get { return Contents; } }
}
