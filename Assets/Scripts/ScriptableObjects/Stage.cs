using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewStage", menuName = "Shopkeep/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] Item[] matsGatherable = null;
    [Tooltip("The icon for this stage.")]
    [SerializeField] Sprite sprite = null;
    [Tooltip("The names of the battle scenes you're taken to when going through the stage.")]
    [SerializeField] string[] battles = null;

    public IList<Item> MatsGatherable { get { return matsGatherable; } }
    public Sprite Sprite { get { return sprite; } }
    public string Name { get { return name; } }
}
