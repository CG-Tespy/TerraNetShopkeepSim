using UnityEngine;

[CreateAssetMenu(fileName = "New Cust Part", menuName = "Shopkeep/Cust Part")]
public class CustPart : Item
{
    [SerializeField] int memoCost = 10;
    public int MemoCost { get { return memoCost; } }
}