using UnityEngine;
using Fungus;

public class NaviController : FighterController<Navi>
{
    /// <summary>
    /// Alias for the fighter type getter.
    /// </summary>
    public Navi Navi { get { return fighter; } }

    public static new System.Action<NaviController> AnyDeath = delegate { };

    [Header("For working with the Flowchart")]
    [SerializeField] protected Flowchart flowchart = null;
    [SerializeField] string hpVarName = "hp";
    [SerializeField] string maxHPVarName = "maxHP";
    [SerializeField] string mugshotVarName = "mugshot";
    [SerializeField] string nameVarName = "name";

    // Flowchart vars
    FloatVariable hpVar, maxHPVar = null;
    SpriteVariable mugshotVar = null;
    StringVariable nameVar = null;

    public override float HP 
    { 
        get => base.HP; 
        set
        {
            base.HP = value;
            hpVar.Value = base.HP;
        }
    }

    public override float MaxHP
    {
        get => base.MaxHP;
        set
        {
            base.MaxHP = value;
            maxHPVar.Value = base.MaxHP;
        }
    }

    public override Sprite Mugshot
    {
        get { return mugshot; }
        protected set
        {
            mugshot = value;
            mugshotVar.Value = value;
        }
    }

    Sprite mugshot = null;

    public virtual string Name
    {
        get { return this.name; }
        set
        {
            this.name = value;
            nameVar.Value = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetUpAnyDeathListener();
        this.Name = Navi.name;
    }

    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        GetDefaultFlowchartAsNeeded();
        hpVar = flowchart.GetVariable<FloatVariable>(hpVarName);
        maxHPVar = flowchart.GetVariable<FloatVariable>(maxHPVarName);
        mugshotVar = flowchart.GetVariable<SpriteVariable>(mugshotVarName);
        nameVar = flowchart.GetVariable<StringVariable>(nameVarName);
    }

    void GetDefaultFlowchartAsNeeded()
    {
        if (flowchart == null)
            flowchart = GetComponent<Flowchart>();
    }

    void SetUpAnyDeathListener()
    {
        Death += () => AnyDeath.Invoke(this);
    }

}
