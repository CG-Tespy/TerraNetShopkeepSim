using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;
using System.Linq;

public class BPLoadoutLoader : SaveLoader<BPLoadoutData>
{
    [Tooltip("Loadouts that can be loaded into")]
    [SerializeField] protected BattlePowerLoadoutDatabase loadoutDatabase;
    [Tooltip("Helps make sure the loadout contents are properly saved")]
    [SerializeField] protected BattlePowerDatabase powerDatabase;

    public override bool Load(BPLoadoutData saveData)
    {
        if (!CanLoad(saveData))
            AlertCannotLoad(saveData);

        RestoreLoadoutStateUsing(saveData);
        return true;
    }

    protected virtual bool CanLoad(BPLoadoutData saveData)
    {
        bool viableLoadoutIndex = saveData.LoadoutIndex >= 0 && saveData.LoadoutIndex < LoadableLoadouts.Count;
        bool viablePowerIndexes = saveData.PowerIndexes.Min() >= 0 &&
            saveData.PowerIndexes.Max() < PowersRegistered.Count;
                                
        return viableLoadoutIndex && viablePowerIndexes;
    }

    protected IList<BattlePowerLoadout> LoadableLoadouts
    {
        get { return loadoutDatabase.Loadouts; }
    }

    protected IList<BattlePower> PowersRegistered { get { return powerDatabase.Powers; } }

    protected virtual void AlertCannotLoad(BPLoadoutData saveData)
    {
        string messageFormat = "Cannot load BP Loadout named {0}. It is either not registered in the database (its index is {1}), or has powers that aren't.";
        string message = string.Format(messageFormat, saveData.LoadoutName, saveData.LoadoutIndex);

        throw new System.InvalidOperationException(message);
    }

    protected virtual void RestoreLoadoutStateUsing(BPLoadoutData saveData)
    {
        BattlePowerLoadout loadout = LoadableLoadouts[saveData.LoadoutIndex];
        loadout.Clear(); // <- Have to wipe the slate clean to properly restore the state
        AddPowersIntoLoadout(loadout, saveData.PowerIndexes);
    }

    protected virtual void AddPowersIntoLoadout(BattlePowerLoadout loadout, IList<int> powerIndexes)
    {
        foreach (int index in powerIndexes)
        {
            BattlePower power = PowersRegistered[index];
            loadout.Add(power);
        }
    }

}
