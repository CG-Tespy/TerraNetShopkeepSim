using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewStage", menuName = "Shopkeep/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] Item[] matsGatherable = null;
    [Tooltip("The icon for this stage.")]
    [SerializeField] Sprite sprite = null;

    public IList<Item> MatsGatherable { get { return matsGatherable; } }
    public Sprite Sprite { get { return sprite; } }
    public string Name { get { return name; } }
}
