using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This helps keep track of the game's BattlePowers, helping the save system
/// identify which are which when doing its thing.
/// </summary>
[CreateAssetMenu(fileName = "NewBattlePowerDatabase", menuName ="Shopkeep/Battle/BattlePowerDatabase")]
public class BattlePowerDatabase : CollectionSO<BattlePower>
{
    /// <summary>
    /// Alias for the contents
    /// </summary>
    public IList<BattlePower> Powers { get { return Contents; } }

}
