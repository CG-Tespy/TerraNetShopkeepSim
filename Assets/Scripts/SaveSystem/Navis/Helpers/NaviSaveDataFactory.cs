public class NaviSaveDataFactory
{
    public static NaviSaveData CreateFrom(Navi navi, NaviDatabase naviDatabase)
    {
        NaviSaveData newData = new NaviSaveData();
        newData.NaviIndex = naviDatabase.IndexOf(navi);
        newData.NaviName = navi.Name;
        newData.HP = navi.HP;
        newData.Atk = navi.Atk;
        newData.Spd = navi.Spd;

        return newData;
    }
}
