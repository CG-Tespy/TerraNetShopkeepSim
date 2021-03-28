using Fungus;

[EventHandlerInfo("Shopkeep", "Battle Power Item Hub Clicked",
    @"An event for when an Item Display Hub component for a Battle Power is clicked.
The itemVar property will be holding the item that the clicked hub was displaying for.")]
public class BattlePowerItemHubClicked : ItemHubClicked
{
    protected override bool ShouldRespondToHub(ItemDisplayHub hub)
    {
        foreach (var holder in hubHolders)
        {
            var holderChildren = holder.GetChildren();

            foreach (var holderChild in holderChildren)
            {
                bool hubHoldsBattlePower = hub.DisplayBase is BattlePower;
                if (holderChild == hub.transform && hubHoldsBattlePower)
                    return true;
            }
        }

        return false;
    }
}
