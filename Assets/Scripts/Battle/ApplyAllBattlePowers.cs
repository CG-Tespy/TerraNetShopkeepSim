using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CommandInfo("Shopkeep/Battle", "Apply All Battle Powers", "Applies the all Battle Powers in the scene to their targets, if they have any,")]
public class ApplyAllBattlePowers : Command
{
    [SerializeField] FloatData timeBetweenUses;

    public override void OnEnter()
    {
        base.OnEnter();

        StartCoroutine(ApplyOneAtATime());
    }

    IEnumerator ApplyOneAtATime()
    {
        var controllers = FindObjectsOfType<BattlePowerController>();

        foreach (var powerController in controllers)
        {
            powerController.ApplyPowerToTarget();
            yield return new WaitForSeconds(timeBetweenUses);
        }

        Continue();
    }
}

public class DamageCalculator
{
    public virtual FighterController Attacker { get; set; }
    public virtual FighterController Target { get; set; }
    public virtual BattlePower PowerUsed { get; set; }
    public virtual bool WeaknessExploited { get; protected set; }

    public virtual int Calculate()
    {
        UpdateAttackElements();
        SetBaseDamage();
        totalDamage = baseDamage;
        ApplyDamageModifiers();
        WeaknessExploited = totalDamage > baseDamage;
        return totalDamage;
    }

    protected virtual void UpdateAttackElements()
    {
        atkElements.Clear(); // To not include elements from previous calculation attempts

        if (PowerUsed != null)
            atkElements.AddRange(PowerUsed.Elements);

    }
    protected virtual IList<Element> atkElements { get; set; } = new List<Element>();

    protected virtual void SetBaseDamage()
    {
        if (PowerUsed == null)
            baseDamage = (int)Attacker.Atk;
        else
            baseDamage = PowerUsed.Damage;

    }

    protected int baseDamage;

    protected virtual void ApplyDamageModifiers()
    {
        if (Target.Weaknesses.ContainsAny(atkElements))
            totalDamage *= 2;
        if (Target.Resistances.ContainsAny(atkElements))
            totalDamage /= 2;
    }

    protected int totalDamage;

}