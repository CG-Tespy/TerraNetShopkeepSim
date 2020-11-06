using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/UI", "Set Item Display", "Sets an ItemDisplayHub to display a specified item.")]
public class SetItemDisplay : Command
{
    [SerializeField] ItemDisplayHub itemDisplay;
    [SerializeField] ItemData item;

    public override void OnEnter()
    {
        base.OnEnter();

        itemDisplay.Item = item;

        Continue();
    }

}
