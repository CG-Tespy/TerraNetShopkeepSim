using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Battlechips/cards/etc. Just using a more generic name in case we decide
/// on some other terminology in-universe.
/// </summary>
[CreateAssetMenu(fileName = "NewBattlePower", menuName = "Shopkeep/Battle/Power")]
public class BattlePower : Item
{
    [SerializeField] Sprite fullArt = null;
    [SerializeField] Element[] elements = null;
    [SerializeField] int damage = 10;
    [SerializeField] int healing = 0;

    public override Sprite Sprite
    {
        get { return FullArt; }
    }

    public Sprite FullArt
    {
        get { return fullArt; }
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

}
