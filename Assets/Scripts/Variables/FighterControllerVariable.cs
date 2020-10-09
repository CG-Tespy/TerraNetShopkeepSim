using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep/Battle", "FighterController")]
[AddComponentMenu("")]
[System.Serializable]
public class FighterControllerVariable : VariableBase<FighterController>
{

}

/// <summary>
/// Container for an FighterControllerDesign variable reference or constant value.
/// </summary>
[System.Serializable]
public struct FighterControllerData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(FighterControllerVariable))]
    public FighterControllerVariable FighterControllerRef;

    [SerializeField]
    public FighterController FighterControllerVal;

    public static implicit operator FighterController(FighterControllerData FighterControllerDesignData)
    {
        return FighterControllerDesignData.Value;
    }

    public FighterControllerData(FighterController v)
    {
        FighterControllerVal = v;
        FighterControllerRef = null;
    }

    public FighterController Value
    {
        get { return (FighterControllerRef == null) ? FighterControllerVal : FighterControllerRef.Value; }
        set { if (FighterControllerRef == null) { FighterControllerVal = value; } else { FighterControllerRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (FighterControllerRef == null)
        {
            return FighterControllerVal != null ? FighterControllerVal.ToString() : "Null";
        }
        else
        {
            return FighterControllerRef.Key;
        }
    }
}
