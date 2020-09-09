using UnityEngine;

/// <summary>
/// The actual item instances based on ItemDesigns. Saves memory
/// and avoids potential issues with duplicating ScriptableObjects
/// and adding them to collections.
/// </summary>
[System.Serializable]
public class Item
{
    [SerializeField] private string name = "";
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private int price = 10;
        
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public Sprite Sprite 
    { 
        get { return sprite; } 
        set { sprite = value; }
    }
    public int Price 
    { 
        get { return price; } 
        set { price = value; }
    }

    public ItemDesign ItemDesign { get; protected set; } = null;

    public static Item From(ItemDesign design)
    {
        Item newItem = new Item
        {
            Name = design.name,
            Sprite = design.Sprite,
            Price = design.Price,
            ItemDesign = design
        };
        
        return newItem;
    }

    public static readonly Item Null = new Item();
}

