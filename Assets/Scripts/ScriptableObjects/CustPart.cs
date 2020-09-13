using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Cust Part", menuName = "Shopkeep/Cust Part")]
public class CustPart : Item
{
    [SerializeField] int memoCost = 10;
    [SerializeField] CustPartClass partClass = null;
    [SerializeField] Item[] matsNeeded = null;

    public int MemoCost { get { return memoCost; } }
    public CustPartClass PartClass { get { return partClass; } }
    public IList<Item> MatsNeeded { get { return matsNeeded; } }

    
}