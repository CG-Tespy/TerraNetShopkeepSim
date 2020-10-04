using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", 
    "Assign Target to Power", 
    "Assigns a target for a BattlePowerController")]
public class AssignTargetToPower : Command
{
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable controller = null;

    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable target = null;

    BattlePowerController power = null;

    public override void OnEnter()
    {
        base.OnEnter();

        SetPower();
        AssignTarget();

        Continue();
    }

    void SetPower()
    {
        power = controller.Value as BattlePowerController;
    }

    void AssignTarget()
    {
        if (power == null)
        {
            Debug.Log("Variable assigned to controller must refer to a BattlePowerController!");
            return;
        }

        power.Target = target.Value as FighterController;
    }
}
