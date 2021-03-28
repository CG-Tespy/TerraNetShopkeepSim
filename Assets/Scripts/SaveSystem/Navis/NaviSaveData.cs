using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

public class NaviSaveData : SaveData
{
    public int NaviIndex
    {
        get { return naviIndex; }
        set { naviIndex = value; }
    }

    [SerializeField] int naviIndex = -1;

    public string NaviName
    {
        get { return naviName; }
        set { naviName = value; }
    }

    [SerializeField] string naviName = "";

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] int hp;

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
