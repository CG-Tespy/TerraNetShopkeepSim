using Fungus;
using UnityEngine;

[EventHandlerInfo("Shopkeep", "Stage Hub Clicked", 
    @"An event for when a Stage Display Hub component is clicked.
The stageVar property will be holding the stage that the clicked hub was displaying for.")]
public class StageHubClicked : EventHandler
{
    [VariableProperty("<Value>", typeof(StageVariable))]
    [SerializeField] StageVariable stageVar = null;

    protected virtual void Awake()
    {
        DisplayHub.AnyClicked.AddListener(OnAnyDisplayHubClicked);
    }

    protected virtual void OnDestroy()
    {
        DisplayHub.AnyClicked.RemoveListener(OnAnyDisplayHubClicked);
    }

    protected virtual void OnAnyDisplayHubClicked(IDisplayHub hub)
    {
        StageDisplayHub stageHub = hub as StageDisplayHub;

        if (stageHub == null)
            return;

        if (stageVar != null)
            stageVar.Value = stageHub.DisplayBase;

        ExecuteBlock();
    }

}
