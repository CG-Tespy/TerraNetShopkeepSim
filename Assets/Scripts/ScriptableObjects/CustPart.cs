using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Cust Part", menuName = "Shopkeep/Cust Part")]
public class CustPart : Item
{
    [SerializeField] int memoCost = 10;
    [SerializeField] Item[] matsNeeded = null;

    public int MemoCost { get { return memoCost; } }
    public IList<Item> MatsNeeded { get { return matsNeeded; } }

    
}