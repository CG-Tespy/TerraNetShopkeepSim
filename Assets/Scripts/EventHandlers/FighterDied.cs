using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep/Battle", 
                "Fighter Died", 
                "Responds to when any fighters in the list die. If the list is empty, this responds to any death.")]
public class FighterDied : EventHandler
{

    [SerializeField] protected List<FighterController> fighters;

    protected virtual void Awake()
    {
        FighterController.AnyDeath += OnAnyFighterDeath;
    }

    protected virtual void OnAnyFighterDeath(FighterController fighter)
    {
        if (ShouldRespondToDeath(fighter))
            ExecuteBlock();
    }

    protected virtual bool ShouldRespondToDeath(FighterController whatTookDamage)
    {
        return RespondToAnyDeath || fighters.Contains(whatTookDamage);
    }

    bool RespondToAnyDeath { get { return fighters.Count == 0; } }

    protected virtual void OnDestroy()
    {
        FighterController.AnyDeath -= OnAnyFighterDeath;
    }
}
