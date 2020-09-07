using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "Shopkeep/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] ItemDesign[] matsGatherable = null;
    [Tooltip("The icon for this stage.")]
    [SerializeField] Sprite sprite = null;

    public ItemDesign[] MatsGatherable { get { return matsGatherable; } }
    public Sprite Sprite { get { return sprite; } }
    public string Name { get { return name; } }
}
