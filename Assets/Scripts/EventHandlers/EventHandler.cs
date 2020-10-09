using UnityEngine;
using Fungus;

/// <summary>
/// An EventHandler with an interface for that allows it to take certain actions
/// before executing their blocks.
/// </summary>
public abstract class EventHandler<T> : EventHandler
{
    public abstract void Trigger(T arg);

}

public abstract class EventHandler<T, T2> : EventHandler
{
    public abstract void Trigger(T arg, T2 arg2);

}

public interface ITriggerableEventHandler
{
    void Trigger();
    void ExecuteBlock();
}

public interface ITriggerableEventHandler<T>
{
    void Trigger(T arg);
    void ExecuteBlock();
}

public class EventHandlerUtils
{
    public static void TriggerAll<TEventHandler>() where TEventHandler: Object, ITriggerableEventHandler
    {
        TEventHandler[] allEventHandlers = GameObject.FindObjectsOfType<TEventHandler>();

        foreach (var eventHandler in allEventHandlers)
            eventHandler.Trigger();
    }

    public static void TriggerAll<TArg, TEventHandler>(TArg arg) where TEventHandler: EventHandler<TArg>
    {
        TEventHandler[] allEventHandlers = GameObject.FindObjectsOfType<TEventHandler>();

        foreach (var eventHandler in allEventHandlers)
            eventHandler.Trigger(arg);
    }
}
