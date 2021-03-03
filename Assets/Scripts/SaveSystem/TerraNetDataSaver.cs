using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

/// <summary>
/// Base class for savers in TerraNet: Digital Craftsman
/// </summary>
public abstract class TerraNetDataSaver<TSaveData, TToMakeSaveOf> : DataSaver<TSaveData>,
    ISaveCreator<TSaveData, TToMakeSaveOf>,
    IGroupSaver<TSaveData>,
    ISaveItemCreator<TSaveData>
    where TSaveData : SaveData
{
    
    public override IList<SaveDataItem> CreateItems()
    {
        IList<TSaveData> mainSaves = CreateSaves();
        IList<SaveDataItem> items = ConvertToSaveItems(mainSaves);
        return items;
    }

    /// <summary>
    /// Creates the proper SaveData-derived instances
    /// </summary>
    public virtual IList<TSaveData> CreateSaves()
    {
        IList<TSaveData> saves = new List<TSaveData>();

        foreach (TToMakeSaveOf saveTarget in ToSave)
        {
            TSaveData newSave = CreateSave(saveTarget);
            saves.Add(newSave);
        }

        return saves;
    }

    /// <summary>
    /// List of objects this saver is responsible for making save data of.
    /// </summary>
    public abstract IList<TToMakeSaveOf> ToSave { get; }

    public virtual IList<SaveDataItem> ConvertToSaveItems(IList<TSaveData> toConvert)
    {
        IList<SaveDataItem> saveItems = new List<SaveDataItem>();

        foreach (TSaveData save in toConvert)
        {
            SaveDataItem newItem = CreateSaveItem(save);
            saveItems.Add(newItem);
        }

        return saveItems;
    }

    public virtual SaveDataItem CreateSaveItem(TSaveData toMakeFrom)
    {
        string jsonString = JsonUtility.ToJson(toMakeFrom);
        SaveDataItem newItem = new SaveDataItem(saveType.Name, jsonString);
        return newItem;
    }

    public abstract TSaveData CreateSave(TToMakeSaveOf from);

    
}
