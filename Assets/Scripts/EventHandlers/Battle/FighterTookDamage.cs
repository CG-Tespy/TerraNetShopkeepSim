using UnityEngine;
using Fungus;
using System.Collections.Generic;

[EventHandlerInfo("Shopkeep/Battle", 
    "Fighter Took Damage", 
    "Executes when any fighter in the list took damage. If the list is empty, this responds to all fighters taking damage.")]
public class FighterTookDamage : EventHandler
{
    [SerializeField] protected List<FighterController> fighters;

    protected virtual void Awake()
    {
        FighterController.AnyTookDamage += OnAnyFighterDamaged;
    }

    protected virtual void OnAnyFighterDamaged(FighterController fighter, float amount)
    {
        if (ShouldRespondToDamage(fighter))
            ExecuteBlock();
    }

    protected virtual bool ShouldRespondToDamage(FighterController whatTookDamage)
    {
        return RespondToAnyDamage || fighters.Contains(whatTookDamage);
    }

    bool RespondToAnyDamage { get { return fighters.Count == 0; } }

    protected virtual void OnDestroy()
    {
        FighterController.AnyTookDamage -= OnAnyFighterDamaged;
    }

}
