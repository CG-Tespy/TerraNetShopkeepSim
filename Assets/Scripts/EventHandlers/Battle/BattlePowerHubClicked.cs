using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep/Battle", "Power Hub Clicked",
    @"An event for when a Battle Power Display Hub component is clicked.
The objVar property will be holding the power that the clicked hub was displaying for.")]
public class BattlePowerHubClicked : DisplayHubClicked<BattlePowerDisplayHub, BattlePower>
{
    [Header("Output vars")]
    [VariableProperty("<Value>", typeof(ObjectVariable))]
    [SerializeField] ObjectVariable controllerVar = null;

    [VariableProperty("<Value>", typeof(GameObjectVariable))]
    [SerializeField] GameObjectVariable gameObjectVar;

    protected override void AssignValuesToVarsFrom(BattlePowerDisplayHub hub)
    {
        base.AssignValuesToVarsFrom(hub);

        // Each displayer should be alongside a controller, so...
        if (controllerVar != null)
            controllerVar.Value = hub.GetComponent<BattlePowerController>();

        if (gameObjectVar != null)
            gameObjectVar.Value = hub.gameObject;
    }
}
