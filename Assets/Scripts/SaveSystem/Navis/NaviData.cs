using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

/// <summary>
/// Save data for the parts of a Navi's state
/// </summary>
public class NaviData : SaveData
{
    public int NaviIndex
    {
        get { return naviIndex; }
        set { naviIndex = value; }
    }

    [SerializeField] int naviIndex = -1;

    public int Atk
    {
        get { return atk; }
        set { atk = value; }
    }
    [SerializeField] int atk;

    public int Spd
    {
        get { return spd; }
        set { spd = value; }
    }
    [SerializeField] int spd;

}
