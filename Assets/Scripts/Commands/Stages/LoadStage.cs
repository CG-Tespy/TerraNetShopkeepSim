using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep", "Load Stage", "Loads a stage.")]
public class LoadStage : LoadScene
{
    [SerializeField] StageData stage;

    public override void OnEnter()
    {
        _sceneName.Value = stage.Value.name;

        base.OnEnter();
    }
}
