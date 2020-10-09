using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayComponent<TDisplayFor> : DisplayComponent<TDisplayFor>
{
    protected virtual void Awake()
    {
        if (displaySpriteOn == null)
            displaySpriteOn = GetComponent<Image>();

        UpdateDisplay();
    }

    [SerializeField] protected Image displaySpriteOn = null;
    protected override void UpdateDisplay()
    {
        if (DisplayBase != null)
            displaySpriteOn.sprite = (DisplayBase as dynamic).Sprite; 
    }
}
