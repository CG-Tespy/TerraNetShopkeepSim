using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Basically for making blueprints of items in the editor.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Shopkeep/Item")]
public class Item : ScriptableObject
{
    [SerializeField] Sprite sprite = null;
    [SerializeField] string displayName = "";
    [SerializeField] int price = 10;
    [SerializeField] Rarity rarity = null;
    [SerializeField] EnumScriptableObject[] classes = null;
    [TextArea(5, 10)]
    [SerializeField] string description = "";

    public virtual Sprite Sprite { get { return sprite; } }
    public virtual int Price { get { return price; } }
    public virtual string Name { get { return name; } }
    public virtual string DisplayName
    {
        get
        {
            if (string.IsNullOrEmpty(displayName))
                return Name;
            else
                return displayName;
        }
    }
    public virtual Rarity Rarity { get { return rarity; } }
    public virtual IList<EnumScriptableObject> Classes { get { return classes; } }
    public virtual string Description { get { return description; } }

}
