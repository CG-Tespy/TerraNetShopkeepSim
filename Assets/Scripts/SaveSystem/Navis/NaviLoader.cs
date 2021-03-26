using UnityEngine;

public class NaviLoader : TerraNetSaveLoader<NaviSaveData>
{
    [Tooltip("Contains the navis that can be loaded.")]
    [SerializeField] NaviDatabase naviDatabase;

    protected override void AlertCannotLoad(NaviSaveData saveData)
    {
        string message = string.Format(loadFailMessageFormat, saveData.NaviName);
        throw new System.InvalidOperationException(message);
    }

    protected static readonly string loadFailMessageFormat = "Cannot load Navi: {0}";

    protected override bool CanLoad(NaviSaveData saveData)
    {
        bool indexNegative = saveData.NaviIndex < 0;
        bool indexInBounds = !indexNegative && saveData.NaviIndex < naviDatabase.Count();
        bool validIndex = indexInBounds;

        return validIndex;
    }

    protected override void RestoreStateWith(NaviSaveData saveData)
    {
        Navi navi = GetNaviBasedOnIndexFrom(saveData);
        RestoreStatsFor(navi, saveData);
    }

    protected virtual Navi GetNaviBasedOnIndexFrom(NaviSaveData saveData)
    {
        return naviDatabase.Contents[saveData.NaviIndex];
    }

    protected virtual void RestoreStatsFor(Navi navi, NaviSaveData hasStats)
    {
        navi.HP = hasStats.HP;
        navi.Atk = hasStats.Atk;
        navi.Spd = hasStats.Spd;
    }
}
