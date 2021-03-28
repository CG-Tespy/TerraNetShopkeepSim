using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public abstract class TerraNetSaveLoader<TSaveData> : SaveLoader<TSaveData>
    where TSaveData: SaveData
{
    public override bool Load(TSaveData saveData)
    {
        if (!CanLoad(saveData))
            AlertCannotLoad(saveData);

        RestoreStateWith(saveData);
        return true;
    }

    protected abstract bool CanLoad(TSaveData saveData);

    /// <summary>
    /// What the loader does when it finds it cannot load the data it was passed.
    /// </summary>
    protected abstract void AlertCannotLoad(TSaveData saveData);

    protected abstract void RestoreStateWith(TSaveData saveData);

}
