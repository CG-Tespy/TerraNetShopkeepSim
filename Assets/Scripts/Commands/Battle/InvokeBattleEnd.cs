using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle",
                 "Invoke Battle End",
                 @"Triggers all BattleEnded event handlers.")]
[AddComponentMenu("")]
public class InvokeBattleEnd : Command
{
    public override void OnEnter()
    {
        base.OnEnter();
        foreach (var eventHandler in GameObject.FindObjectsOfType<BattleEnded>())
            eventHandler.Trigger();
    }
}
