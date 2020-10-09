using UnityEngine;

public class EnumScriptableObject : ScriptableObject
{
    [SerializeField] Sprite icon = null;

    public string Name { get { return name; } }
    public Sprite Icon { get { return icon; } }
}
