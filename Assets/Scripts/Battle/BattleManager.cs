using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Flowchart flowchart = null;
    [SerializeField] string targetClickedName = null;

    ObjectVariable targetClickedVariable = null;
    public FighterController TargetClicked { get { return targetClickedVariable.Value as FighterController; } }

    protected virtual void Awake()
    {
        if (flowchart == null)
            flowchart = GetComponent<Flowchart>();

        targetClickedVariable = flowchart.GetVariable<ObjectVariable>(targetClickedName);
    }


}
