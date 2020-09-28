using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep/Battle", "EnemyController")]
[AddComponentMenu("")]
[System.Serializable]
public class EnemyControllerVariable : VariableBase<EnemyController>
{

}

/// <summary>
/// Container for an EnemyControllerDesign variable reference or constant value.
/// </summary>
[System.Serializable]
public struct EnemyControllerData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(EnemyControllerVariable))]
    public EnemyControllerVariable EnemyControllerRef;

    [SerializeField]
    public EnemyController EnemyControllerVal;

    public static implicit operator EnemyController(EnemyControllerData EnemyControllerDesignData)
    {
        return EnemyControllerDesignData.Value;
    }

    public EnemyControllerData(EnemyController v)
    {
        EnemyControllerVal = v;
        EnemyControllerRef = null;
    }

    public EnemyController Value
    {
        get { return (EnemyControllerRef == null) ? EnemyControllerVal : EnemyControllerRef.Value; }
        set { if (EnemyControllerRef == null) { EnemyControllerVal = value; } else { EnemyControllerRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (EnemyControllerRef == null)
        {
            return EnemyControllerVal != null ? EnemyControllerVal.ToString() : "Null";
        }
        else
        {
            return EnemyControllerRef.Key;
        }
    }
}
