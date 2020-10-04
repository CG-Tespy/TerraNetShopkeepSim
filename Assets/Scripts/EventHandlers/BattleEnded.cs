using Fungus;

[EventHandlerInfo("Shopkeep/Battle", "Battle Ended",
    @"An event for when a battle ends.")]
public class BattleEnded : EventHandler
{
    public virtual void Trigger()
    {
        ExecuteBlock();
    }
}
