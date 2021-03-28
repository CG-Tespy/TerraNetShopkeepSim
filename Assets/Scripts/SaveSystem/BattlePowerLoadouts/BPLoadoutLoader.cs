using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;
using System.Linq;

public class BPLoadoutLoader : TerraNetSaveLoader<BPLoadoutData>
{
    [Tooltip("Loadouts that can be loaded into")]
    [SerializeField] protected BattlePowerLoadoutDatabase loadoutDatabase;
    [Tooltip("Helps make sure the loadout contents are properly saved")]
    [SerializeField] protected BattlePowerDatabase powerDatabase;

    protected override bool CanLoad(BPLoadoutData saveData)
    {
        bool viableLoadoutIndex = saveData.LoadoutIndex >= 0 && saveData.LoadoutIndex < LoadableLoadouts.Count;
        bool loadoutIsEmpty = saveData.PowerIndexes.Count == 0;
        bool viablePowerIndexes = loadoutIsEmpty || (saveData.PowerIndexes.Min() >= 0 &&
            saveData.PowerIndexes.Max() < PowersRegistered.Count);
                                
        return viableLoadoutIndex && viablePowerIndexes;
    }

    protected IList<BattlePowerLoadout> LoadableLoadouts
    {
        get { return loadoutDatabase.Loadouts; }
    }

    protected IList<BattlePower> PowersRegistered { get { return powerDatabase.Powers; } }

    protected override void AlertCannotLoad(BPLoadoutData saveData)
    {
        string messageFormat = "Cannot load BP Loadout named {0}. It is either not registered in the database (its index is {1}), or has powers that aren't.";
        string message = string.Format(messageFormat, saveData.LoadoutName, saveData.LoadoutIndex);

        throw new System.InvalidOperationException(message);
    }

    protected override void RestoreStateWith(BPLoadoutData saveData)
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
