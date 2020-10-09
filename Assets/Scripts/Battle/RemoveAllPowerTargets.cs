using Fungus;

[CommandInfo("Shopkeep/Battle", "Remove All Power Targets", "Makes it so all Battle Powers in the scene have no targets.")]
public class RemoveAllPowerTargets : Command
{
    public override void OnEnter()
    {
        base.OnEnter();

        var allPowers = FindObjectsOfType<BattlePowerController>();

        foreach (var power in allPowers)
            power.Target = null;

        Continue();
    }
}
