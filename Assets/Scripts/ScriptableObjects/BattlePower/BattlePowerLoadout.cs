using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBattlePowerLoadout", menuName = "Shopkeep/Battle/BattlePowerLoadout")]
public class BattlePowerLoadout : CollectionSO<BattlePower>
{
    /// <summary>
    /// Alias for the contents.
    /// </summary>
    public IList<BattlePower> Powers
    {
        get { return Contents; }
    }


}
