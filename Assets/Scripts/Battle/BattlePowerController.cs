using System;
using UnityEngine;

/// <summary>
/// Provides an interface for using Battle Powers shown in the UI.
/// </summary>
[RequireComponent(typeof(BattlePowerDisplayHub))]
public class BattlePowerController : MonoBehaviour
{
    [Header("For visualization")]
    [SerializeField] FighterController target = null;

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

    protected BattlePowerDisplayHub displayer = null;

    protected virtual void Awake()
    {
        displayer = GetComponent<BattlePowerDisplayHub>();
    }

    public void ApplyPowerToTarget()
    {
        if (target == null)
            return;


        target.TakeDamage(Power.Damage);
        target.TakeHealing(Power.Healing);
    }
}
