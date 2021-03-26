using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class NaviSaver : TerraNetDataSaver<NaviSaveData, Navi>,
    ISaveCreator<NaviSaveData, Navi>,
    IGroupSaver<NaviSaveData>
{
    [SerializeField] NaviDatabase naviDatabase;
    public override IList<Navi> ToSave
    {
        get { return naviDatabase.Contents; }
    }

    public override NaviSaveData CreateSave(Navi navi)
    {
        return NaviSaveDataFactory.CreateFrom(navi, naviDatabase);
    }
}
