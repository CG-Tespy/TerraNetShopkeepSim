using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", 
            "Deal Damage", 
            "Deals damage to the specified target. Should be a var pointing to a FighterController.")]
public class DealDamage : Command
{
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable target = null;
    [SerializeField] FloatData damage;

    public override void OnEnter()
    {
        base.OnEnter();
        FighterController fighter = target.Value as FighterController;
        fighter.TakeDamage(damage);
        Continue();
    }
}
