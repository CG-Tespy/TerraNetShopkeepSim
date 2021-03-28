using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Action = System.Action;

[CommandInfo("Shopkeep/Battle", 
    "Remove from BPL", "Removes from Battle Power Loadouts")]
public class RemoveFromBPL : Command
{
    public enum RemovalMethod
    {
        individualPowers,
        index
    }

    [SerializeField] protected RemovalMethod removalMethod;

    [SerializeField] protected BattlePowerLoadout[] toRemoveFrom;

    [Tooltip("Applies when the removal method is Individual Powers")]
    [SerializeField] protected BattlePower[] powersToRemove;

    [Tooltip("Applies when the removal method is Index")]
    [SerializeField] protected IntegerData index;

    protected virtual void Awake()
    {
        SetUpMethodDict();
    }

    protected virtual void SetUpMethodDict()
    {
        MethodDict[RemovalMethod.individualPowers] = RemoveIndividuals;
        MethodDict[RemovalMethod.index] = RemoveByIndexes;
    }

    protected Dictionary<RemovalMethod, Action> MethodDict = new Dictionary<RemovalMethod, Action>();
    // ^To cut down on if statements, we'll execute the removal methods using this

    protected virtual void RemoveIndividuals()
    {
        foreach (BattlePowerLoadout loadout in toRemoveFrom)
        {
            loadout.RemoveRange(powersToRemove);
        }
    }

    protected virtual void RemoveByIndexes()
    {
        foreach (BattlePowerLoadout loadout in toRemoveFrom)
        {
            loadout.RemoveAt(index);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();

        var RemoveAsNeeded = MethodDict[removalMethod];
        RemoveAsNeeded();

        Continue();
    }

    
}
