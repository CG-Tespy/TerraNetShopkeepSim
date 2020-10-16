using Fungus;
using UnityEngine;
using System.Text.RegularExpressions;

[RequireComponent(typeof(Flowchart))]
public class Customer : MonoBehaviour
{
    protected Flowchart flowchart = null;

    [Header("For working with the Flowchart")]
    [SerializeField] string nameVarName = "name";
    [SerializeField] string descVarName = "description";
    [SerializeField] string mugshotVarName = "mugshot";
    [SerializeField] string itemPrefsVarName = "itemPrefs";
    [SerializeField] string weaknessesVarName = "weaknesses", resistancesVarName = "resistances";
    [SerializeField] string baseBudgetVarName = "baseBudget";
    [SerializeField] string budgetVarName = "budget";
    [SerializeField] string wantedItemVarName = "wantedItem";

    [TextArea(5, 10)]
    [Tooltip("Better to set the desc here than in the Flowchart.")]
    [SerializeField] string description = "";
    
    protected virtual void Awake()
    {
        SetUpComponents();
    }

    protected virtual void SetUpComponents()
    {
        flowchart = GetComponent<Flowchart>();
        FetchVariables();
        ApplyValuesToVars();
    }

    

    protected virtual void FetchVariables()
    {
        nameVar = flowchart.GetVariable<StringVariable>(nameVarName);
        descVar = flowchart.GetVariable<StringVariable>(descVarName);
        mugshot = flowchart.GetVariable<SpriteVariable>(mugshotVarName);
        itemPrefs = flowchart.GetVariable<CollectionVariable>(itemPrefsVarName);
        weaknesses = flowchart.GetVariable<CollectionVariable>(weaknessesVarName);
        resistances = flowchart.GetVariable<CollectionVariable>(resistancesVarName);
        baseBudgetVar = flowchart.GetVariable<IntegerVariable>(baseBudgetVarName);
        budgetVar = flowchart.GetVariable<IntegerVariable>(budgetVarName);
        wantedItem = flowchart.GetVariable<ObjectVariable>(wantedItemVarName);
    }

    StringVariable nameVar = null;
    StringVariable descVar = null;
    SpriteVariable mugshot = null;
    // These should hold instances of ItemClass objects
    CollectionVariable itemPrefs = null, weaknesses = null, resistances = null;
    IntegerVariable baseBudgetVar = null, budgetVar = null;
    ObjectVariable wantedItem = null;

    protected virtual void ApplyValuesToVars()
    {
        // The ones that are supposed to be set through script, rather than Flowchart
        nameVar.Value = base.name;
        RemoveDescCarriageReturns();
        descVar.Value = description;
    }

    protected virtual void RemoveDescCarriageReturns()
    {
        // Unity just adds them in for some reason...
        description = Regex.Replace(description, @"\r+", "");
    }

    public virtual new string name
    {
        get { return nameVar.Value; }
        set { nameVar.Value = value; }
    }

    public virtual string Description
    {
        get { return descVar.Value; }
    }

    public virtual Sprite Mugshot
    {
        get { return mugshot.Value; }
    }

    /// <summary>
    /// The types of items this Customer prefers.
    /// </summary>
    public virtual Collection ItemPrefs
    {
        get { return itemPrefs.Value; }
    }
        
    /// <summary>
    /// The types of persuasion techniques that work best on this customer.
    /// </summary>
    public virtual Collection Weaknesses
    {
        get { return weaknesses.Value; }
    }

    /// <summary>
    /// The types of persuasion techniques that don't work well on this customer.
    /// </summary>
    public virtual Collection Resistances
    {
        get { return resistances.Value; }
    }

    public virtual int BaseBudget
    {
        get { return baseBudgetVar.Value; }
    }

    public virtual int Budget
    {
        get { return budgetVar.Value; }
        set { budgetVar.Value = value; }
    }

    public virtual Item WantedItem
    {
        get { return wantedItem.Value as Item; }
        set { wantedItem.Value = value; }
    }

    public virtual bool WantsAnything
    {
        get { return wantedItem.Value != null; }
    }


}
