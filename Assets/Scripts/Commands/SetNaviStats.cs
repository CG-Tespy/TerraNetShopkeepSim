using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Stats", "Set Navi Stats", "Sets the specified navi's stats based on the specified method")]
public class SetNaviStats : Command
{
    public enum SetMethod
    {
        staticValues,
        increaseBy
    }

    [SerializeField] SetMethod setMethod = SetMethod.increaseBy;
    [SerializeField] protected Navi navi;
    [SerializeField] protected IntegerData hp, atk, spd;

    public override void OnEnter()
    {
        base.OnEnter();

        if (setMethod == SetMethod.staticValues)
        {
            navi.HP = hp;
            navi.Atk = atk;
            navi.Spd = spd;
        }
        else if (setMethod == SetMethod.increaseBy)
        {
            navi.HP += hp;
            navi.Atk += atk;
            navi.Spd += spd;
        }

        Continue();
    }

}
