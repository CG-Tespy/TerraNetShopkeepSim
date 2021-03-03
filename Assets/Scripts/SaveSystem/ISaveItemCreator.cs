using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public interface ISaveItemCreator<TSaveData>
    where TSaveData: SaveData
{
    SaveDataItem CreateSaveItem(TSaveData toMakeFrom);
}
