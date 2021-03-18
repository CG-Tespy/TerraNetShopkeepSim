using UnityEngine;
using Fungus;

[CommandInfo("Shopkeep/Battle", 
    "Add to BPLs", 
    "Adds the specificed Battle Powers (from Object and Item vars) to the specified BPLs.")]
public class AddToBPLs : Command
{
    [SerializeField] ObjectData[] objPowers;
    [SerializeField] ItemData[] itemPowers;

    [SerializeField] BattlePowerLoadout[] loadouts;

    public override void OnEnter()
    {
        base.OnEnter();

        foreach (Object objEl in objPowers)
        {
            foreach (var loadoutEl in loadouts)
            {
                loadoutEl.Add(objEl as BattlePower);
            }
        }

        foreach (Item itemEl in itemPowers)
        {
            foreach (var loadoutEl in loadouts)
            {
                loadoutEl.Add(itemEl as BattlePower);
            }
        }

        Continue();
    }
}
