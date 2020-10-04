using UnityEngine;
using Action = System.Action;

public abstract class FighterController : MonoBehaviour, IFighterController
{
    [SerializeField] protected float hp = 10;
    [SerializeField] protected float maxHP = 10;

    public abstract string Name { get; }
    public abstract string Description { get; }

    public virtual float HP
    {
        get { return hp; }
        set
        {
            if (hp <= 0)
                return;

            hp = Mathf.Clamp(value, 0, MaxHP);

            if (this.IsDead)
            {
                Debug.Log(this.name + " deleted!");
                Death.Invoke();
            }

        }
    }

    public virtual float MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = Mathf.Clamp(value, float.Epsilon, float.MaxValue);
        }
    }

    public virtual float Atk { get; protected set; }
    public virtual float Spd { get; protected set; }
    public abstract Sprite Mugshot { get; protected set; }

    public event Action Death = delegate { };
    public static event FighterHandler AnyDeath = delegate { };
    public event DamageHandler TookDamage = delegate { };
    public static event FighterDamageHandler AnyTookDamage = delegate { };

    public virtual bool IsDead
    {
        get { return hp <= 0; }
    }


    protected virtual void Awake()
    {
        SetUpComponents();
        SetUpDeathListeners();
        
    }

    protected virtual void SetUpComponents()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer SpriteRenderer { get; protected set; } = null;

    void SetUpDeathListeners()
    {
        Death += () => AnyDeath.Invoke(this);
        Death += WhenThisDies;
    }
    

    protected virtual void WhenThisDies() 
    {

    }

    protected virtual void OnDestroy()
    {
        Death -= WhenThisDies;
    }

    public virtual void TakeDamage(float amount)
    {
        if (HP <= 0)
            return;

        HP -= amount;
        AnyTookDamage.Invoke(this, amount);
        TookDamage.Invoke(amount);
            
    }

    public virtual void TakeHealing(float amount)
    {
        hp += amount;
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
        MaxHP = fighter.HP;
        HP = fighter.HP;
        Atk = fighter.Atk;
        Spd = fighter.Spd;
        Mugshot = fighter.Mugshot;
    }
}

public interface IFighterController
{
    float HP { get; }
    float MaxHP { get; }
    float Atk { get; }
    float Spd { get; }
}

public delegate void DamageHandler(float amount);
public delegate void FighterHandler(FighterController fighter);
public delegate void FighterDamageHandler(FighterController fighter, float damage);