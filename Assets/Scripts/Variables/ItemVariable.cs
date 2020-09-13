using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep", "Item")]
[AddComponentMenu("")]
[System.Serializable]
public class ItemVariable : VariableBase<Item>
{

}

/// <summary>
/// Container for an ItemDesign variable reference or constant value.
/// </summary>
[System.Serializable]
public struct ItemData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(ItemVariable))]
    public ItemVariable itemRef;

    [SerializeField]
    public Item itemVal;

    public static implicit operator Item(ItemData ItemDesignData)
    {
        return ItemDesignData.Value;
    }

    public ItemData(Item v)
    {
        itemVal = v;
        itemRef = null;
    }

    public Item Value
    {
        get { return (itemRef == null) ? itemVal : itemRef.Value; }
        set { if (itemRef == null) { itemVal = value; } else { itemRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (itemRef == null)
        {
            return itemVal != null ? itemVal.ToString() : "Null";
        }
        else
        {
            return itemRef.Key;
        }
    }
}
