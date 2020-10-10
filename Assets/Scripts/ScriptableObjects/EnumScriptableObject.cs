using UnityEngine;

public class EnumScriptableObject : ScriptableObject
{
    [SerializeField] Sprite icon = null;
    [TextArea(5, 10)]
    [SerializeField] string description = "";

    public string Name { get { return name; } }
    public Sprite Icon { get { return icon; } }
    public string Description { get { return description; } }
}
