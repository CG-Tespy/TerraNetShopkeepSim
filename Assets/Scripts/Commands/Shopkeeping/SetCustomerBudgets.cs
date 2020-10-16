using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Set Customer Budgets", "Sets the budgets of all customers parented to the passed transform.")]
public class SetCustomerBudgets : Command
{
    [SerializeField] Collection customers = null;
    [SerializeField] FloatData multiplier = new FloatData(1f);

    public override void OnEnter()
    {
        base.OnEnter();

        SetBudgets();
        
        Continue();
    }

    protected virtual void SetBudgets()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            Customer customerEl = (Customer) customers[i];
            int baseBudget = customerEl.BaseBudget;
            customerEl.Budget = (int)(baseBudget * multiplier);
        }
    }

}
