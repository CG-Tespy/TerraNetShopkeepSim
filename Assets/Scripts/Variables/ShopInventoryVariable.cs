using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep", "ShopInventory")]
[AddComponentMenu("")]
[System.Serializable]
public class ShopInventoryVariable : VariableBase<ShopInventory>
{

}


[System.Serializable]
public struct ShopInventoryData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(ShopInventoryVariable))]
    public ShopInventoryVariable ShopInventoryRef;

    [SerializeField]
    public ShopInventory ShopInventoryVal;

    public static implicit operator ShopInventory(ShopInventoryData ShopInventoryData)
    {
        return ShopInventoryData.Value;
    }

    public ShopInventoryData(ShopInventory v)
    {
        ShopInventoryVal = v;
        ShopInventoryRef = null;
    }

    public ShopInventory Value
    {
        get { return (ShopInventoryRef == null) ? ShopInventoryVal : ShopInventoryRef.Value; }
        set { if (ShopInventoryRef == null) { ShopInventoryVal = value; } else { ShopInventoryRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (ShopInventoryRef == null)
        {
            return ShopInventoryVal != null ? ShopInventoryVal.ToString() : "Null";
        }
        else
        {
            return ShopInventoryRef.Key;
        }
    }
}