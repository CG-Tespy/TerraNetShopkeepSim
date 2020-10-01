using Fungus;
using UnityEngine;

/**
 * Provides at least part of the interface necessary for the Fungus VS system
 * to make enemies do their thing.
 */ 
[ExecuteInEditMode]
public class EnemyController : FighterController<EnemyType>, IEnemy
{
    /// <summary>
    /// Alias for the fighter type getter.
    /// </summary>
    public EnemyType Type { get { return Fighter; } }

    public static System.Action<EnemyController> AnyDeath = delegate { };

    [Header("For working with the Flowchart")]
    [SerializeField] protected Flowchart flowchart = null;
    [SerializeField] string enemyTypeVarName = "enemyType";

    Clickable2D clickable = null;

    protected override void Awake()
    {
        base.Awake();
        SetUpAnyDeathListener();
        GetDefaultFlowchartAsNeeded();
        GiveFlowchartVariableData();
    }

    void SetUpAnyDeathListener()
    {
        Death += () => AnyDeath.Invoke(this);
    }

    void GetDefaultFlowchartAsNeeded()
    {
        if (flowchart == null)
            flowchart = GetComponent<Flowchart>();
    }

    void GiveFlowchartVariableData()
    {
        ObjectVariable enemyTypeVar = flowchart.GetVariable<ObjectVariable>(enemyTypeVarName);
        if (enemyTypeVar != null)
            enemyTypeVar.Value = this.Type;
    }

    protected virtual void Update()
    {
        if (!Application.isPlaying)
            TurnIntoFighter();
    }

    protected override void WhenThisDies()
    {
        base.WhenThisDies();
        this.gameObject.SetActive(false);
    }


}

public interface IEnemy
{
    EnemyType Fighter { get; }
}

public delegate void EnemyHandler(EnemyController enemy);
public delegate void IEnemyHandler(IEnemy enemy);