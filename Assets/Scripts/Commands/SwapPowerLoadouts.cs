using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Swap Power Loadouts", 
    "Has 2 Battle Power Controllers swap the loadouts they belong to, as well as their sibling indexes. " +
    "Does not actually change the contents of said loadouts; it just changes values in the Controllers.")]
public class SwapPowerLoadouts : Command
{
    [VariableProperty(typeof(GameObjectVariable))]
    [SerializeField] GameObjectVariable firstPower, secondPower;

    [Header("Sibling indexes of the powers in their original loadouts.")]
    [SerializeField] IntegerData firstIndex;
    [SerializeField] IntegerData secondIndex;

    public override void OnEnter()
    {
        base.OnEnter();

        GetControllers();
        GetLoadouts();
        SwapLoadoutValues();
        SwapSiblingIndexes();
        
        Continue();
    }

    protected virtual void GetControllers()
    {
        firstController = firstPower.Value.GetComponent<BattlePowerController>();
        secondController = secondPower.Value.GetComponent<BattlePowerController>();
    }

    protected BattlePowerController firstController, secondController;

    protected virtual void GetLoadouts()
    {
        firstLoadout = firstController.Loadout;
        secondLoadout = secondController.Loadout;
    }
    
    protected BattlePowerLoadout firstLoadout, secondLoadout;

    protected virtual void SwapLoadoutValues()
    {
        firstController.Loadout = secondLoadout;
        secondController.Loadout = firstLoadout;
    }

    protected virtual void SwapSiblingIndexes()
    {
        firstController.transform.SetSiblingIndex(secondIndex);
        secondController.transform.SetSiblingIndex(firstIndex);
    }
}
