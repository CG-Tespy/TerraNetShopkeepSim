using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Item", "Get Base Price", "Assigns the base price of an item to a variable.")]
public class GetBasePrice : Command
{
    [SerializeField] ItemData item;
    [VariableProperty(typeof(IntegerVariable))]
    [SerializeField] IntegerVariable priceVar = null;

    public override void OnEnter()
    {
        base.OnEnter();

        Item item = (Item)this.item;
        priceVar.Value = item.Price;

        Continue();
    }
}
