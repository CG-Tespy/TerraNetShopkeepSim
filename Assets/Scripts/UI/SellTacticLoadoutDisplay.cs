public class SellTacticLoadoutDisplay : LoadoutDisplay<SellTacticDisplayHub, SellTacticLoadout, SellTactic>
{
    protected override void DisplayLoadout()
    {
        var tactics = toDisplay.Tactics;

        for (int i = 0; i < tactics.Count; i++)
        {
            var tacticEl = tactics[i];
            AddDisplayHubWith(tacticEl);
        }
    }

    protected virtual void AddDisplayHubWith(SellTactic tactic)
    {
        var newHub = Instantiate(hubPrefab, displayHolder, false);
        newHub.Tactic = tactic;
    }
}
