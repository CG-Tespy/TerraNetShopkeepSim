using UnityEngine;
using Fungus;

[VariableInfo("Shopkeep", "Stage")]
[AddComponentMenu("")]
[System.Serializable]
public class StageVariable : VariableBase<Stage>
{

}


[System.Serializable]
public struct StageData
{
    [SerializeField]
    [VariableProperty("<Value>", typeof(StageVariable))]
    public StageVariable stageRef;

    [SerializeField]
    public Stage stageVal;

    public static implicit operator Stage(StageData StageData)
    {
        return StageData.Value;
    }

    public StageData(Stage v)
    {
        stageVal = v;
        stageRef = null;
    }

    public Stage Value
    {
        get { return (stageRef == null) ? stageVal : stageRef.Value; }
        set { if (stageRef == null) { stageVal = value; } else { stageRef.Value = value; } }
    }

    public string GetDescription()
    {
        if (stageRef == null)
        {
            return stageVal != null ? stageVal.ToString() : "Null";
        }
        else
        {
            return stageRef.Key;
        }
    }
}