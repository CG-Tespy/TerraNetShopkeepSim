using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", "Get BP from Hub", 
    "Gets a Battle Power from a Display Hub, assigning it to certain variables")]
public class GetBattlePowerFromHub : Command
{
    [VariableProperty(typeof(GameObjectVariable))]
    [SerializeField] protected GameObjectVariable hasHub;

    [Header("Output vars")]
    [VariableProperty(typeof(GameObjectVariable))]
    [SerializeField] protected GameObjectVariable gameObjectVar;

    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] protected ObjectVariable objectVar;

    [VariableProperty(typeof(ItemVariable))]
    [SerializeField] protected ItemVariable itemVar;

    public override void OnEnter()
    {
        base.OnEnter();

        DisplayHub hub = hasHub.Value.GetComponent<DisplayHub>();

        bool isBattlePowerHub = hub is BattlePowerDisplayHub;
        bool isItemHub = hub is ItemDisplayHub;
        bool isNeither = !isBattlePowerHub && !isItemHub;

        if (isNeither)
        {
            LetUserKnowOfInvalidHub();
            return;
        }

        if (gameObjectVar != null)
            gameObjectVar.Value = hub.gameObject;

        if (objectVar != null)
            objectVar.Value = hub.GetDisplayBase();

        if (itemVar != null)
            itemVar.Value = hub.GetDisplayBase() as Item;
  
        Continue();
    }

    protected virtual void LetUserKnowOfInvalidHub()
    {
        string message = "Invalid hub passed to GetBattlePowerFromHub!";
        throw new System.InvalidOperationException(message);
    }
}
