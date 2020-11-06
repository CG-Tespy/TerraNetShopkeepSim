using UnityEngine;
using Fungus;

[CommandInfo("Scripting",
                 "Toggle Local Active",
                 "Reverses the local active state of a game object in the scene.")]
[AddComponentMenu("")]
public class ToggleLocalActive : Command
{
    [Tooltip("Reference to game object to enable / disable")]
    [SerializeField] protected GameObjectData _targetGameObject;

    public override void OnEnter()
    {
        base.OnEnter();

        var gameObject = _targetGameObject.Value;
        bool newActive = !gameObject.activeSelf;
        gameObject.SetActive(newActive);

        Continue();
    }

    public override Color GetButtonColor()
    {
        return new Color32(235, 191, 217, 255);
    }
}
