using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Load Stage", "Loads a stage.")]
public class LoadStage : LoadScene
{
    [SerializeField] StageData stage;

    public override void OnEnter()
    {
        Object firstBattleInStage = stage.Value.Battles[0];
        _sceneName.Value = firstBattleInStage.name;

        base.OnEnter();
    }
}
