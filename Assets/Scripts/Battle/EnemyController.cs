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

    public static new event EnemyHandler AnyDeath = delegate { };

    [Header("For working with the Flowchart")]
    [SerializeField] protected Flowchart flowchart = null;
    [SerializeField] string enemyTypeVarName = "enemyType";
    [SerializeField] string atkVarName = "atk";
    [SerializeField] string takeActionBlockName = "TakeAction";
    [SerializeField] string mugshotVarName = "mugshot";

    public override Sprite Mugshot
    {
        get { return mugshot; }
        protected set
        {
            mugshot = value;
            mugshotVar.Value = value;
        }
    }

    Sprite mugshot;
    protected SpriteVariable mugshotVar = null;

    Clickable2D clickable = null;

    protected override void Awake()
    {
        base.Awake();
        SetUpAnyDeathListener();
        GetDefaultFlowchartAsNeeded();
        GiveFlowchartVariableData();

    }

    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        mugshotVar = flowchart.GetVariable<SpriteVariable>(mugshotVarName);
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

        FloatVariable atkVar = flowchart.GetVariable<FloatVariable>(atkVarName);
        atkVar.Value = this.Atk;
    }


    protected virtual void Update()
    {
        if (!Application.isPlaying)
            TurnIntoFighter();
    }

    protected override void WhenThisDies()
    {
        base.WhenThisDies();
    }

    /// <summary>
    /// Executes the Take Action block
    /// </summary>
    public virtual void TakeAction()
    {
        if (!this.IsDead && this.isActiveAndEnabled)
            flowchart.ExecuteBlock(takeActionBlockName);
    }

}

public interface IEnemy
{
    EnemyType Fighter { get; }
}

public delegate void EnemyHandler(EnemyController enemy);
public delegate void IEnemyHandler(IEnemy enemy);