using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Customer", 
    "Calculate Pitch Effectiveness", 
    "Based on the sell tactics, calculates pitch effectivness, setting the end multiplier to the var.")]
public class CalculatePitchEffectiveness : Command
{
    [SerializeField] protected CollectionData sellTactics;
    [VariableProperty(typeof(FloatVariable))]
    [SerializeField] protected FloatVariable endMultiplier = null;
    [SerializeField] protected ObjectData customer;

    protected Collection SellTactics { get { return sellTactics.Value; } }
    protected Customer Customer { get; set; }
    protected float multiplier = 1;
    protected SellTactic tactic = null;
    protected Customer.TacticResponse response;

    public override void OnEnter()
    {
        base.OnEnter();

        Reset();
        DoTheCalculations();

        endMultiplier.Value = multiplier;
        
        Continue();
    }

    protected virtual void Reset()
    {
        // The same copy of this command can be run multiple times, making things like the
        // multiplier carry over. So, we need this to start on a clean slate every time
        // it's to be executed.
        ResetCustomer();
        ResetMultiplier();
    }

    protected virtual void ResetCustomer()
    {
        Customer = (Customer) customer.Value;
    }

    protected virtual void ResetMultiplier()
    {
        multiplier = 1;
    }

    protected virtual void DoTheCalculations()
    {
        for (int i = 0; i < SellTactics.Count; i++)
        {
            tactic = (SellTactic) SellTactics[i];
            response = Customer.ResponseTo(tactic);

            HaveResponseAffectMultiplier();
        }
    }

    protected virtual void HaveResponseAffectMultiplier()
    {
        if (response == Customer.TacticResponse.positive)
            multiplier += (tactic.SuccessBoost / 100f);

        else if (response == Customer.TacticResponse.negative)
            multiplier -= (tactic.FailurePenalty / 100f);
    }
}
