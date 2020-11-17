using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", "Get Battle Power Fields", "Lets you assign certain Battle Power values to Fungus variables.")]
public class GetBattlePowerFields : Command
{
    [VariableProperty(typeof(ObjectVariable))]
    [Tooltip("The one we will get the fields from.")]
    [SerializeField] ObjectVariable battlePower = null;

    [VariableProperty(typeof(SpriteVariable))]
    [SerializeField] protected SpriteVariable fullArt = null;

    [VariableProperty(typeof(IntegerVariable))]
    [SerializeField] protected IntegerVariable damage = null;

    [VariableProperty(typeof(IntegerVariable))]
    [SerializeField] protected IntegerVariable healing = null;

    [SerializeField] protected CollectionData elements;

    public override void OnEnter()
    {
        base.OnEnter();

        if (battlePower != null)
            AssignToVariablesAsNeeded();
        else
            Debug.LogWarning("There is no battle power assigned to GetBattlePowerFields!");

        Continue();

    }

    protected virtual void AssignToVariablesAsNeeded()
    {
        BattlePower battlePower = this.battlePower.Value as BattlePower;

        if (fullArt != null)
            fullArt.Value = battlePower.FullArt;

        AssignIntegersFrom(battlePower);
        AssignElementsFrom(battlePower);
    }

    protected virtual void AssignIntegersFrom(BattlePower battlePower)
    {
        if (damage != null)
            damage.Value = battlePower.Damage;

        if (healing != null)
            healing.Value = battlePower.Healing;
    }

    protected virtual void AssignElementsFrom(BattlePower battlePower)
    {
        if (this.elements.Value != null)
        {
            ObjectCollection elements = this.elements.Value as ObjectCollection;
            elements.Clear();

            foreach (Element elementEl in battlePower.Elements)
            {
                elements.Add(elementEl);
            }
        }
    }
}
