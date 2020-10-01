using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Events", 
    "Alert Enemy Clicked", 
    "Triggers all the EnemyClicked events, passing along the specified EnemyController.")]
public class AlertEnemyClicked : Command
{
    [SerializeField] EnemyControllerData enemy;

    public override void Execute()
    {
        base.Execute();

        if (enemy.Value != null)
            EventHandlerUtils.TriggerAll<EnemyController, EnemyClicked>(enemy);
        else
            throw new System.ArgumentException("Need an enemy to pass in Alert Enemy Clicked!");

        Continue();
    }
}
