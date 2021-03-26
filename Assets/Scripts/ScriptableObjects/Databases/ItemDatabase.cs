
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Shopkeep/ItemDatabase")]
public class ItemDatabase : CollectionSO<Item>
{
    /// <summary>
    /// Alias for the Contents
    /// </summary>
    public virtual IList<Item> Items { get { return Contents; } }
}
