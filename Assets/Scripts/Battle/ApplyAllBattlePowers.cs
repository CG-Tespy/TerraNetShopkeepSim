using Fungus;
using System.Collections;
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