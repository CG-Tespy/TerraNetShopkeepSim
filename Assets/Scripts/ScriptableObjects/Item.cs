using UnityEngine;

/// <summary>
/// Basically for making blueprints of items in the editor.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Shopkeep/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private int price = 10;
    [SerializeField] Rarity rarity = null;
    [TextArea(5, 10)]
    [SerializeField] string description = "";

    public Sprite Sprite { get { return sprite; } }
    public int Price { get { return price; } }
    public string Name { get { return name; } }
    public Rarity Rarity { get { return rarity; } }
    public string Description { get { return description; } }


}
