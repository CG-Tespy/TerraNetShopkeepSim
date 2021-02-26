using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helps keep track of the loadouts meant to be saved
/// </summary>
[CreateAssetMenu(fileName = "NewBattlePowerLoadoutDatabase", menuName = "Shopkeep/Battle/BattlePowerLoadoutDatabase")]
public class BattlePowerLoadoutDatabase : ScriptableObject
{
    [SerializeField] protected List<BattlePowerLoadout> loadouts;
    public IList<BattlePowerLoadout> Loadouts
    {
        get { return loadouts; }
    }

    public virtual int IndexOf(BattlePowerLoadout loadoutArg)
    {
        return Loadouts.IndexOf(loadoutArg);
    }
}
