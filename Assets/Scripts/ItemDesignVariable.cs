using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep", "ItemDesign")]
[AddComponentMenu("")]
[System.Serializable]
public class ItemDesignVariable : VariableBase<ItemDesign>
{

}

/// <summary>
/// Container for an Animator variable reference or constant value.
/// </summary>
[System.Serializable]
public struct ItemDesignData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(ItemDesignVariable))]
    public ItemDesignVariable ItemDesignRef;

    [SerializeField]
    public ItemDesign ItemDesignVal;

    public static implicit operator ItemDesign(ItemDesignData ItemDesignData)
    {
        return ItemDesignData.Value;
    }

    public ItemDesignData(ItemDesign v)
    {
        ItemDesignVal = v;
        ItemDesignRef = null;
    }

    public ItemDesign Value
    {
        get { return (ItemDesignRef == null) ? ItemDesignVal : ItemDesignRef.Value; }
        set { if (ItemDesignRef == null) { ItemDesignVal = value; } else { ItemDesignRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (ItemDesignRef == null)
        {
            return ItemDesignVal != null ? ItemDesignVal.ToString() : "Null";
        }
        else
        {
            return ItemDesignRef.Key;
        }
    }
}
