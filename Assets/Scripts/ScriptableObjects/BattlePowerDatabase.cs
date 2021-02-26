using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This helps keep track of the game's BattlePowers, helping the save system
/// identify which are which when doing its thing.
/// </summary>
[CreateAssetMenu(fileName = "NewBattlePowerDatabase", menuName ="Shopkeep/Battle/BattlePowerDatabase")]
public class BattlePowerDatabase : ScriptableObject
{
    [SerializeField] BattlePower[] powers = { };

    public IList<BattlePower> Powers { get { return powers; } }

    public virtual bool Contains(BattlePower power)
    {
        return Powers.Contains(power);
    }

    public virtual int IndexOf(BattlePower power)
    {
        return Powers.IndexOf(power);
    }
}
