using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Customer", "Get Wanted Item", "Assigns a Customer's wanted item to an variable.")]
public class GetWantedItem : Command
{
    [SerializeField] ObjectData customer;
    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] ItemVariable var = null;

    public override void OnEnter()
    {
        base.OnEnter();

        var customer = (Customer) this.customer.Value;
        var.Value = customer.WantedItem;

        Continue();
    }
}
