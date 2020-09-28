using UnityEngine;
using Fungus;

[EventHandlerInfo("Shopkeep/Battle", "Power Hub Clicked",
    @"An event for when a Battle Power Display Hub component is clicked.
The powerVar property will be holding the power that the clicked hub was displaying for.")]
public class BattlePowerHubClicked : DisplayHubClicked<BattlePowerDisplayHub, BattlePower>
{

}
