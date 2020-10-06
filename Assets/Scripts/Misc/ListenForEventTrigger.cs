using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ListenForEventTrigger : MonoBehaviour
{
    EventTrigger trigger = null;
    EventTrigger.Entry entry = new EventTrigger.Entry();

    // Start is called before the first frame update
    void Awake()
    {
        trigger = GetComponent<EventTrigger>();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener(OnPointerEnterDelegate);
        trigger.triggers.Add(entry);
    }

    public void OnPointerEnterDelegate(BaseEventData data)
    {
        OnPointerEnter(data as PointerEventData);
    }

    void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Pointer Enter called");
    }

    
}
