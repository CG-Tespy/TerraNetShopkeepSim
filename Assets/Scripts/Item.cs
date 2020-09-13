using UnityEngine;

/// <summary>
/// Basically for making blueprints of items in the editor.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Shopkeep/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private int price = 10;

    public Sprite Sprite { get { return sprite; } }
    public int Price { get { return price; } }
    public string Name { get { return name; } }

}
