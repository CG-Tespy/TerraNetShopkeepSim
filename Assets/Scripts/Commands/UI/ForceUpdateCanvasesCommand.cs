using UnityEngine;
using Fungus;

[CommandInfo("UI", "Force Update Canvases", "Forces all canvases on screen to refresh.")]
public class ForceUpdateCanvasesCommand : Command
{
    public override void OnEnter()
    {
        base.OnEnter();
        Canvas.ForceUpdateCanvases();
        Continue();
    }
}
