using UnityEngine;
using Fungus;

[CommandInfo("Variable", "Get Item Sprite", "Sets the sprite of an item to a Sprite variable.")]
public class GetItemSprite : Command
{
    [SerializeField] ItemData item;
    [VariableProperty(typeof(SpriteVariable))]
    [SerializeField] SpriteVariable spriteVar = null;

    public override void OnEnter()
    {
        base.OnEnter();
        spriteVar.Value = item.Value.Sprite;
        Continue();
    }
}
