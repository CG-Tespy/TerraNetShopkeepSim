using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Inventory", "Get Item Values", "Lets you assign certain Item values into variables.")]
public class GetItemValues : Command
{
    [SerializeField] protected ItemData item;

    [VariableProperty(typeof(StringVariable))]
    [SerializeField] protected StringVariable displayNameVar = null;
    [VariableProperty(typeof(StringVariable))]
    [SerializeField] protected StringVariable descriptionVar = null;

    [VariableProperty(typeof(SpriteVariable))]
    [SerializeField] protected SpriteVariable spriteVar = null;

    [VariableProperty(typeof(IntegerVariable))]
    [SerializeField] protected IntegerVariable priceVar = null;

    public override void OnEnter()
    {
        base.OnEnter();

        Item item = this.item.Value;

        if (displayNameVar != null)
            displayNameVar.Value = item.DisplayName;

        if (descriptionVar != null)
            descriptionVar.Value = item.Description;

        if (spriteVar != null)
            spriteVar.Value = item.Sprite;

        if (priceVar != null)
            priceVar.Value = item.Price;

        Continue();
    }
}
