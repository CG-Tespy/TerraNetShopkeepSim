using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class BPLoadoutSaver : TerraNetDataSaver<BPLoadoutData, BattlePowerLoadout>, 
    ISaveCreator<BPLoadoutData, BattlePowerLoadout>, 
    IGroupSaver<BPLoadoutData>
{
    [Tooltip("The loadouts that will get saved")]
    [SerializeField] protected BattlePowerLoadoutDatabase loadoutDatabase;
    [Tooltip("Helps make sure the loadout contents are properly saved")]
    [SerializeField] protected BattlePowerDatabase powerDatabase;

    /// <summary>
    /// Creates BPLoadoutData instances from the loadouts this saver is set to save.
    /// </summary>
    public override IList<BPLoadoutData> CreateSaves()
    {
        RemoveNullLoadoutsAndLetUserKnow();

        return base.CreateSaves();
    }

    protected virtual void RemoveNullLoadoutsAndLetUserKnow()
    {
        int loadoutCountBefore = Loadouts.Count;
        Loadouts.RemoveNulls();
        int loadoutCountAfter = Loadouts.Count;

        if (loadoutCountAfter != loadoutCountBefore)
        {
            Debug.LogWarning("Null BP Loadouts found in BPLoadoutSaver. Nulls removed from Loadout Database.");
        }
    }

    protected IList<BattlePowerLoadout> Loadouts { get { return loadoutDatabase.Loadouts; } }

    public override IList<BattlePowerLoadout> ToSave
    {
        get { return Loadouts; }
    }

    public override BPLoadoutData CreateSave(BattlePowerLoadout loadout)
    {
        return BPLoadoutData.From(loadout, powerDatabase, loadoutDatabase);
    }

}
