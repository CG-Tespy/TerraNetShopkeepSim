using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep", "Item")]
[AddComponentMenu("")]
[System.Serializable]
public class ItemVariable : VariableBase<Item>
{

}

/// <summary>
/// Container for an Animator variable reference or constant value.
/// </summary>
[System.Serializable]
public struct ItemData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(ItemVariable))]
    public ItemVariable ItemRef;

    [SerializeField]
    public Item ItemVal;

    public static implicit operator Item(ItemData ItemData)
    {
        return ItemData.Value;
    }

    public ItemData(Item v)
    {
        ItemVal = v;
        ItemRef = null;
    }

    public Item Value
    {
        get { return (ItemRef == null) ? ItemVal : ItemRef.Value; }
        set { if (ItemRef == null) { ItemVal = value; } else { ItemRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (ItemRef == null)
        {
            return ItemVal != null ? ItemVal.ToString() : "Null";
        }
        else
        {
            return ItemRef.Key;
        }
    }
}
