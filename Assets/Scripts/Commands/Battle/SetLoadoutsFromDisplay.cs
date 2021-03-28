using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", 
    "Set Loadout From Display", 
    "Sets the contents of a loadout based on the BattlePowerControllers parented to a transform.")]
public class SetLoadoutsFromDisplay : Command
{
    [SerializeField] protected TransformData hasPowers;
    [SerializeField] ObjectData loadout;

    public override void OnEnter()
    {
        base.OnEnter();
        var powerControllers = hasPowers.Value.GetComponentsInChildren<BattlePowerController>();

        var actualLoadout = loadout.Value as BattlePowerLoadout;
        actualLoadout.Contents.Clear();

        foreach (var controller in powerControllers)
        {
            actualLoadout.Add(controller.Power);
        }

        Continue();
    }

}
