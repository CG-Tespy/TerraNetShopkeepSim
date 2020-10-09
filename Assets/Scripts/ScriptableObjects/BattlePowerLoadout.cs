using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBattlePowerLoadout", menuName = "Shopkeep/Battle/BattlePowerLoadout")]
public class BattlePowerLoadout : ScriptableObject
{
    [Tooltip("Which Battle Powers this starts off with.")]
    [SerializeField] BattlePower[] defaultContents = { };
    [Tooltip("What powers are in this loadout at any given time.")]
    [SerializeField] private List<BattlePower> powers = new List<BattlePower>();

    public IList<BattlePower> Powers { get { return powers; } }


    protected virtual void OnEnable()
    {
        Powers.Clear();
        foreach (BattlePower power in defaultContents)
        {
            if (power != null)
                Powers.Add(power);
        }
    }


}
