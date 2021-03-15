using UnityEngine;
using Fungus;
using System.Collections.Generic;

public enum DeathResponse
{
    any,
    type,
    individual
}

[EventHandlerInfo("Shopkeep/Battle", "Enemy Died",
    @"An event for when an enemy dies. The enemyVar property will be holding the enemy that died.")]
public class EnemyDied : EventHandler
{
    [VariableProperty("<Value>", typeof(ObjectVariable))]
    [SerializeField] ObjectVariable enemyVar = null;

    [SerializeField] EnemyType type = null;
    [SerializeField] EnemyController individual = null;
    [SerializeField] DeathResponse respondTo = DeathResponse.any;

    protected virtual void Awake()
    {
        SetUpResponses();
    }

    protected virtual void SetUpResponses()
    {
        EnemyController.AnyDeath += WhenAnyEnemyDies;
        responses[DeathResponse.any] = RespondToAnyDeath;
        responses[DeathResponse.type] = RespondBasedOnType;
        responses[DeathResponse.individual] = RespondToIndividualDeath;
    }

    protected virtual void WhenAnyEnemyDies(EnemyController enemy)
    {
        EnemyHandler respondToDeath = responses[this.respondTo];
        respondToDeath(enemy);
    }

    Dictionary<DeathResponse, EnemyHandler> responses = new Dictionary<DeathResponse, EnemyHandler>();

    protected virtual void RespondToAnyDeath(EnemyController enemy)
    {
        ExecuteBlockWith(enemy);
    }

    protected virtual void ExecuteBlockWith(EnemyController enemy)
    {
        if (enemyVar != null)
            enemyVar.Value = enemy;

        this.ExecuteBlock();
    }

    protected virtual void RespondBasedOnType(EnemyController enemy)
    {
        if (enemy.Fighter == this.type)
            ExecuteBlockWith(enemy);
    }

    protected virtual void RespondToIndividualDeath(EnemyController enemy)
    {
        if (enemy == individual)
            ExecuteBlockWith(enemy);
    }

    private void OnDestroy()
    {
        EnemyController.AnyDeath -= WhenAnyEnemyDies;
    }
}
