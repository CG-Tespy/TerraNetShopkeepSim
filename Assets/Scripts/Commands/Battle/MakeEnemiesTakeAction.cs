using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", 
    "Make Enemies Take Action", 
    "Makes the enemies parented to the target Transform attack their target(s).")]
public class MakeEnemiesTakeAction : Command
{
    [SerializeField] protected Transform enemyParent = null;

    public override void OnEnter()
    {
        base.OnEnter();

        if (enemyParent == null)
        {
            Continue();
            return;
        }

        EnemyController[] enemies = enemyParent.GetComponentsInChildren<EnemyController>();

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyController enemyEl = enemies[i];
            enemyEl.TakeAction();
        }

        Continue();
    }
}
