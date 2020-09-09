using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("UI",
                 "Alter Canvas Group",
                 @"Lets you change certain Canvas Group fields.")]
[AddComponentMenu("")]
public class SetCanvasGroup : Command
{
    [SerializeField] protected CanvasGroup canvasGroup;

    [SerializeField] protected FloatData alpha;
    [SerializeField] protected BooleanData interactable;
    [SerializeField] protected BooleanData blocksRaycasts;
    [SerializeField] protected BooleanData ignoreParentGroups;

    public override void OnEnter()
    {
        base.OnEnter();
        canvasGroup.alpha = alpha.Value;
        canvasGroup.interactable = interactable.Value;
        canvasGroup.blocksRaycasts = blocksRaycasts.Value;
        canvasGroup.ignoreParentGroups = ignoreParentGroups.Value;
        Continue();
    }
}
