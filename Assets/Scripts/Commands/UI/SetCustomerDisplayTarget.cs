using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Set CustomerDisplay Target", 
    "Sets the customer a CustomerDisplay handles through an Object value.")]
public class SetCustomerDisplayTarget : Command
{
    [SerializeField] ObjectData holdsCustomer;
    [SerializeField] CustomerDisplay display = null;

    public override void OnEnter()
    {
        base.OnEnter();
        display.Customer = holdsCustomer.Value as Customer;

        Continue();
    }
}
