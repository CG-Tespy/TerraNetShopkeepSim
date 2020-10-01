using UnityEngine;
using Action = System.Action;

public abstract class FighterController : MonoBehaviour, IFighterController
{
    [SerializeField] protected float hp = 10;
    [SerializeField] protected float maxHP = 10;

    public abstract string Name { get; }
    public abstract string Description { get; }

    public float HP
    {
        get { return hp; }
        set
        {
            if (hp <= 0)
                return;

            hp = Mathf.Clamp(value, 0, MaxHP);

            if (hp <= 0)
            {
                Debug.Log(this.name + " died!");
                Death.Invoke();
            }

        }
    }

    public float MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = Mathf.Clamp(value, float.Epsilon, float.MaxValue);
        }
    }

    public float Atk { get; protected set; }
    public float Spd { get; protected set; }
    public abstract Sprite Mugshot { get; }

    public event Action Death = delegate { };

    protected virtual void Awake()
    {
        SetUpComponents();
        Death += WhenThisDies;
    }

    protected virtual void SetUpComponents()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer SpriteRenderer { get; protected set; } = null;

    protected virtual void WhenThisDies() { }

    protected virtual void OnDestroy()
    {
        Death -= WhenThisDies;
    }
}

/// <summary>
/// Base class for things that can fight, as represented in MonoBehaviours.
/// </summary>
public abstract class FighterController<TFighterType> : FighterController, IFighterController
    where TFighterType : FighterType
{
    [SerializeField] protected TFighterType fighter = null;
    

    public TFighterType Fighter => fighter;

    public override string Name
    {
        get { return Fighter.name; }
    }

    public override string Description
    {
        get { return Fighter.Description; }
    }

    public override Sprite Mugshot
    {
        get { return fighter.Mugshot; }
    }

    protected override void Awake()
    {
        base.Awake();
        TurnIntoFighter();
    }

    protected virtual void TurnIntoFighter()
    {
        if (fighter == null)
            return;

        SpriteRenderer.sprite = fighter.BattleSprite;
        // For debug
        maxHP = fighter.HP;
        hp = fighter.HP;
        Atk = fighter.Atk;
        Spd = fighter.Spd;
    }
}

public interface IFighterController
{
    float HP { get; }
    float MaxHP { get; }
    float Atk { get; }
    float Spd { get; }
}