using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class BPLoadoutSaver : DataSaver<BPLoadoutData>, ISaveCreator<BPLoadoutData, BattlePowerLoadout>, IGroupSaver<BPLoadoutData>
{
    [Tooltip("The loadouts that will get saved")]
    [SerializeField] protected BattlePowerLoadoutDatabase loadoutDatabase;
    [Tooltip("Helps make sure the loadout contents are properly saved")]
    [SerializeField] protected BattlePowerDatabase powerDatabase;

    public override IList<SaveDataItem> CreateItems()
    {
        IList<SaveDataItem> items = new List<SaveDataItem>();
        var loadoutSaves = CreateSaves();

        foreach (var save in loadoutSaves)
        {
            var newItem = CreateItem(save);
            items.Add(newItem);
        }

        return items;
    }

    /// <summary>
    /// Creates BPLoadoutData instances from the loadouts this saver is set to save.
    /// </summary>
    public IList<BPLoadoutData> CreateSaves()
    {
        RemoveNullLoadoutsAndLetUserKnow();

        var saves = new BPLoadoutData[Loadouts.Count];

        for (int i = 0; i < Loadouts.Count; i++)
        {
            BattlePowerLoadout currentLoadout = Loadouts[i];
            BPLoadoutData newData = CreateSave(currentLoadout);
            saves[i] = newData;
        }

        return saves;
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

    public BPLoadoutData CreateSave(BattlePowerLoadout loadout)
    {
        return BPLoadoutData.From(loadout, powerDatabase, loadoutDatabase);
    }

    public virtual SaveDataItem CreateItem(BPLoadoutData data)
    {
        var jsonString = JsonUtility.ToJson(data);
        var newItem = new SaveDataItem(saveType.Name, jsonString);
        return newItem;
    }

}
