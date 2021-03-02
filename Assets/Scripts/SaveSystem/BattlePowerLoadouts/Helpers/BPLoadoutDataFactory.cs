using System.Collections.Generic;

public class BPLoadoutDataFactory
{
    public static BPLoadoutData CreateFrom(BattlePowerLoadout loadout, BattlePowerDatabase powerDatabase,
        BattlePowerLoadoutDatabase loadoutDatabase)
    {
        BPLoadoutData result = new BPLoadoutData();

        result.PowerIndexes = GetIndexesOfPowers(loadout, powerDatabase);
        result.LoadoutName = loadout.name;
        result.LoadoutIndex = loadoutDatabase.IndexOf(loadout);

        return result;
    }

    protected static IList<int> GetIndexesOfPowers(BattlePowerLoadout loadout, BattlePowerDatabase database)
    {
        IList<int> indexes = new List<int>();

        foreach (BattlePower power in loadout.Contents)
        {
            if (!database.Contains(power))
                // We have a problem... We need to make sure that the database has everything in the loadout
                AlertDatabaseNotHaving(power, database);

            indexes.Add(database.IndexOf(power));
        }

        return indexes;
    }

    protected static void AlertDatabaseNotHaving(BattlePower power, BattlePowerDatabase database)
    {
        string messageFormat = "BattlePowerDatabase {0} does not have the BattlePower {1}";
        string message = string.Format(messageFormat, database.name, power.name);
        throw new System.ArgumentException(message);
    }
}
