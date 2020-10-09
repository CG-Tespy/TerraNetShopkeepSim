/**
 * Interface for things that can fight.
 */ 
public interface IFighter : IMortal
{
    string Name { get; }
    string Description { get; }
    float Atk { get; }
    float Spd { get; }
}

public interface IMortal
{
    float HP { get; }
    float MaxHP { get; }

}
