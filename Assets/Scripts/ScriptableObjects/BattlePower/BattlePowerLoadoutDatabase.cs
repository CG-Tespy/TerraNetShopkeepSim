using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helps keep track of the loadouts meant to be saved
/// </summary>
[CreateAssetMenu(fileName = "NewBattlePowerLoadoutDatabase", menuName = "Shopkeep/Battle/BattlePowerLoadoutDatabase")]
public class BattlePowerLoadoutDatabase : CollectionSO<BattlePowerLoadout>
{
    /// <summary>
    /// Alias for the contents
    /// </summary>
    public IList<BattlePowerLoadout> Loadouts
    {
        get { return Contents; }
    }

}
