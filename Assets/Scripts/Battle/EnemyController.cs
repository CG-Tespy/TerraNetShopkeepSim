using UnityEngine;

/**
 * Provides at least part of the interface necessary for the Fungus VS system
 * to make enemies do their thing.
 */ 
[ExecuteInEditMode]
public class EnemyController : FighterController<EnemyType>, IEnemy
{
    public static System.Action<EnemyController> AnyDeath = delegate { };

    protected override void Awake()
    {
        base.Awake();
        SetUpAnyDeathListener();
    }

    void SetUpAnyDeathListener()
    {
        Death += () => AnyDeath.Invoke(this);
    }

    protected virtual void Update()
    {
        if (!Application.isPlaying)
            TurnIntoFighter();
    }

}

public interface IEnemy
{
    EnemyType Fighter { get; }
}

public delegate void EnemyHandler(EnemyController enemy);
public delegate void IEnemyHandler(IEnemy enemy);