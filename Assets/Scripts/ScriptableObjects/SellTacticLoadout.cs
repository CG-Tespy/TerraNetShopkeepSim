using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSellTacticLoadout", menuName = "Shopkeep/SellTacticLoadout")]
public class SellTacticLoadout : CollectionSO<SellTactic>
{
    /// <summary>
    /// Alias for the contents.
    /// </summary>
    public IList<SellTactic> Tactics
    {
        get { return Contents; }
    }
}
