using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory",
    "Clear Battle Power Loadouts",
    @"Removes all contents of the specified loadouts.")]
[AddComponentMenu("")]
public class ClearBattlePowerLoadouts : Command
{
    [SerializeField] protected ObjectData[] loadouts = { };

    public override void OnEnter()
    {
        base.OnEnter();

        foreach (var loadoutEl in this.loadouts)
        {
            BattlePowerLoadout actualLoadout = loadoutEl.Value as BattlePowerLoadout;
            actualLoadout.Contents.Clear();
        }

        Continue();
    }
}
