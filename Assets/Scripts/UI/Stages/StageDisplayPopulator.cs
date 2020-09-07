using UnityEngine;

/// <summary>
/// Handles creating stage-related display components.
/// </summary>
public abstract class StageDisplayPopulator : MonoBehaviour
{
    [SerializeField] Transform[] displayPrefabs = null;
    [SerializeField] Transform displayHolder = null;

    protected virtual void Awake()
    {
        Populate();
    }

    protected abstract void Populate();

}
