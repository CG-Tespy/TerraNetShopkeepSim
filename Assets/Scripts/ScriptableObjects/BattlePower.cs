using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Battlechips/cards/etc. Just using a more generic name in case we decide
/// on some other terminology in-universe.
/// </summary>
[CreateAssetMenu(fileName = "NewBattlePower", menuName = "Shopkeep/Battle/Power")]
public class BattlePower : ScriptableObject
{
    [SerializeField] Sprite thumbnail = null;
    [SerializeField] Sprite fullArt = null;
    [SerializeField] Rarity rarity = null;
    [SerializeField] BattlePowerClass _class = null;
    [SerializeField] Element[] elements = null;
    [SerializeField] int damage = 10;
    [SerializeField] int healing = 0;
    [SerializeField] Item[] materialsToCraft = null;

    public string Name
    {
        get { return name; }
    }
    public Sprite Thumbnail
    {
        get { return thumbnail; }
    }

    public Sprite FullArt
    {
        get { return fullArt; }
    }

    public Rarity Rarity
    {
        get { return rarity; }
    }

    public BattlePowerClass Class
    {
        get { return _class; }
    }

    public IList<Element> Elements
    {
        get { return elements; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public int Healing
    {
        get { return healing; }
    }

    public Item[] MaterialsToCraft
    {
        get { return materialsToCraft; }
    }
}
