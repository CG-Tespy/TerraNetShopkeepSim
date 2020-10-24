using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helps manage the minigame.
/// </summary>
public class ShopkeepMinigame : MonoBehaviour
{
    [SerializeField] Transform sellTacticHolder = null;
    [SerializeField] SellTacticDisplayHub displayPrefab = null;

    List<SellTacticDisplayHub> tacticDisplays = new List<SellTacticDisplayHub>();

    public virtual void SetSellTacticsToDisplay(SellTacticLoadout loadout)
    {
        ErasePreviousTacticDisplays();
        CreateTacticDisplaysFrom(loadout);
    }

    protected virtual void ErasePreviousTacticDisplays()
    {
        while (tacticDisplays.Count > 0)
        {
            var display = tacticDisplays[0];
            Destroy(display.gameObject);
        }
    }

    protected virtual void CreateTacticDisplaysFrom(SellTacticLoadout loadout)
    {
        var tacticsToSet = loadout.Tactics;

        for (int i = 0; i < tacticsToSet.Count; i++)
        {
            var tactic = tacticsToSet[i];
            var displayer = Instantiate<SellTacticDisplayHub>(displayPrefab);
            displayer.transform.SetParent(sellTacticHolder, false);
            displayer.Tactic = tactic;
            tacticDisplays.Add(displayer);
        }
    }



}
