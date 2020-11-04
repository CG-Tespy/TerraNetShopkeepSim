using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Shopkeep/Crafting/Recipe")]
public class Recipe : Item
{
    [SerializeField] protected Item itemCreated = null;
    [SerializeField] protected Item[] materials = null;

    public virtual Item ItemCreated
    {
        get { return itemCreated; }
    }

    public virtual IList<Item> Materials
    {
        get { return materials; }
    }

    public override Sprite Sprite
    {
        get
        {
            if (base.Sprite == null)
                return itemCreated.Sprite;
            else
                return base.Sprite;
        }
    }

}
