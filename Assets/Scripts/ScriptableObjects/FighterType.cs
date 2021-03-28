using UnityEngine;
using System.Collections.Generic;
using Fungus;

public abstract class FighterType : ScriptableObject
{
    [SerializeField] Sprite battleSprite = null;
    [SerializeField] Sprite mugshot = null;
    [Tooltip("For things like dialogue.")]
    [SerializeField] Character characterPrefab = null;
    [SerializeField] Element[] elements = null;
    [Tooltip("What elements this is weak to.")]
    [SerializeField] Element[] weaknesses = null;
    [Tooltip("What elements this resists.")]
    [SerializeField] Element[] resistances = null;

    [SerializeField] int hp = 10, atk = 10, spd = 10;
    [TextArea(5, 10)]
    [SerializeField] string description = "";

    public string Name 
    { 
        get { return name; } 
        set { name = value; }
    }
    public Sprite BattleSprite { get { return battleSprite; } }
    public Sprite Mugshot { get { return mugshot; } }
    public Character Character { get { return characterPrefab; } }
    public IList<Element> Elements {  get { return elements; } }
    public IList<Element> Weaknesses { get { return weaknesses; } }
    public IList<Element> Resistances { get { return resistances; } }
    public int HP 
    { 
        get { return hp; } 
        set { hp = value; }
    }

    public int Atk 
    { 
        get { return atk; } 
        set { atk = value; }
    }

    public int Spd 
    { 
        get { return spd; } 
        set { spd = value; }
    }
    public string Description 
    { 
        get { return description; } 
        set { description = value; }
    }
}
