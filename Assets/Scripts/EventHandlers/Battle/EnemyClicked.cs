
using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep/Battle", "Enemy Clicked",
    @"An event for when an enemy is clicked.")]
public class EnemyClicked : EventHandler<EnemyController>
{
    [Header("Variable to assign the clicked enemy's controller to.")]
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable enemControlVar = null;

    [Header("Variable to assign the clicked enemy's type to.")]
    [VariableProperty(typeof(ObjectVariable))]
    [SerializeField] ObjectVariable enemyTypeVar = null;

    protected EnemyController enemyClicked = null;

    public override void Trigger(EnemyController enemy)
    {
        enemyClicked = enemy;
        AssignToVarsAsNeeded();
        ExecuteBlock();
    }

    void AssignToVarsAsNeeded()
    {
        if (enemControlVar != null)
            enemControlVar.Value = enemyClicked;
        
        if (enemyTypeVar != null)
            enemyTypeVar.Value = enemyClicked.Type;
    }
}
