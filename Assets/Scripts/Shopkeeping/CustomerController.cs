using Fungus;
using UnityEngine;

[RequireComponent(typeof(Flowchart))]
public class CustomerController : MonoBehaviour
{
    protected Flowchart flowchart = null;

    [Header("For working with the Flowchart")]
    [SerializeField] string nameVarName = "name";
    [SerializeField] string descVarName = "description";
    [SerializeField] string mugshotVarName = "mugshot";
    [SerializeField] string itemPrefsVarName = "itemPrefs";
    [SerializeField] string weaknessesVarName = "weaknesses", resistancesVarName = "resistances";
    [SerializeField] string baseBudgetVarName = "baseBudget";

    [TextArea(5, 10)]
    [SerializeField] string description = "";
    [SerializeField] int baseBudget = 100;
    
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

    public virtual string SomeMethod()
    {
        return "";
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
    }

    StringVariable nameVar = null;
    StringVariable descVar = null;
    SpriteVariable mugshot = null;
    // These should hold instances of ItemClass objects
    CollectionVariable itemPrefs = null, weaknesses = null, resistances = null;
    IntegerVariable baseBudgetVar = null;

    protected virtual void ApplyValuesToVars()
    {
        // The ones that are supposed to be set through script, rather than Flowchart
        nameVar.Value = base.name;
        descVar.Value = description;
        baseBudgetVar.Value = baseBudget;
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

}
