using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class BPLoadoutData : SaveData, ICollectionSOData
{
    // Indexes of the battle powers in the battle power database
    [SerializeField] protected List<int> powerIndexes = new List<int>();
    // Index of the loadout in the loadout database
    [SerializeField] protected int loadoutIndex;
    [SerializeField] protected string loadoutName;

    /// <summary>
    /// Alias for ContentIndexes
    /// </summary>
    public virtual IList<int> PowerIndexes 
    { 
        get { return ContentIndexes; } 
        set { ContentIndexes = value; }
    }

    public IList<int> ContentIndexes
    {
        get { return powerIndexes; }
        set
        {
            // Safety
            if (value == null)
                AlertNullPowerIndexSetting();

            powerIndexes.Clear();
            powerIndexes.AddRange(value);
        }
    }

    protected virtual void AlertNullPowerIndexSetting()
    {
        string message = "Cannot have power indexes be null!";
        throw new System.ArgumentException(message);
    }

    /// <summary>
    /// Alias for CollectionIndex
    /// </summary>
    public virtual int LoadoutIndex
    {
        get { return CollectionIndex; }
        set { CollectionIndex = value; }
    }

    public int CollectionIndex
    {
        get { return loadoutIndex; }
        set { loadoutIndex = value; }
    }

    /// <summary>
    /// Alias for CollectionName
    /// </summary>
    public virtual string LoadoutName
    {
        get { return CollectionName; }
        set { CollectionName = value; }
    }

    public string CollectionName
    {
        get { return loadoutName; }
        set { loadoutName = value; }
    }

    public static BPLoadoutData From(BattlePowerLoadout loadout, 
        BattlePowerDatabase powerDatabase,
        BattlePowerLoadoutDatabase loadoutDatabase)
    {
        return BPLoadoutDataFactory.CreateFrom(loadout, powerDatabase, loadoutDatabase);
    }


}
