using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep", "Battle Ended",
    @"An event for when a battle ends.")]
public class BattleEnded : EventHandler
{
    public virtual void Trigger()
    {
        ExecuteBlock();
    }
}
