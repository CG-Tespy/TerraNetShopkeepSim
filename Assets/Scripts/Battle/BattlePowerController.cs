using System;
using UnityEngine;

/// <summary>
/// Represents a Battle Power on a GameObject.
/// </summary>
[RequireComponent(typeof(BattlePowerDisplayHub))]
public class BattlePowerController : MonoBehaviour
{
    [SerializeField] FighterController user = null;
    [Header("For visualization")]
    [SerializeField] FighterController target = null;
    [Tooltip("Loadout this belongs to")]
    [SerializeField] BattlePowerLoadout loadout = null;

    public virtual FighterController Target
    {
        get { return target; }
        set
        {
            target = value;
            TargetSet(value);
        }
    }
    

    public event Action<FighterController> TargetSet = delegate { };
    public virtual BattlePower Power
    {
        get { return displayer.BattlePower; }
    }

    public virtual FighterController User
    {
        get { return user; }
        set { user = value; }
    }

    /// <summary>
    /// The loadout the power this is representing belongs to.
    /// </summary>
    public virtual BattlePowerLoadout Loadout
    {
        get { return loadout; }
        set { loadout = value; }
    }

    protected BattlePowerDisplayHub displayer = null;

    protected virtual void Awake()
    {
        displayer = GetComponent<BattlePowerDisplayHub>();
    }

    public void ApplyPowerToTarget()
    {
        if (target == null)
            return;

        int totalDamage = CalculateDamage();
        target.TakeDamage(totalDamage);

        target.TakeHealing(Power.Healing);
    }

    protected virtual int CalculateDamage()
    {
        damageCalculator.Target = target;
        damageCalculator.Attacker = this.User;
        damageCalculator.PowerUsed = Power;
        int totalDamage = damageCalculator.Calculate();
        return totalDamage;
    }

    protected DamageCalculator damageCalculator = new DamageCalculator();
}
