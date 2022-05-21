using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitData
{
    public int VictimRank;
    public GameObject Offender;

    public HitData(int rank, GameObject offender)
    {
        VictimRank = rank;
        Offender = offender;
    }
}

public class CustomEventData
{
    public float floatContent;
    public int intContent;
    public string stringContent;
    public HitData hit;

    public CustomEventData(float value)
    {
        floatContent = value;
    }

    public CustomEventData(int value)
    {
        intContent = value;
    }

    public CustomEventData(string value)
    {
        stringContent = value;
    }

    public CustomEventData(HitData value)
    {
        hit = value;
    }
}

[System.Serializable]
public class CustomEvent : UnityEvent<CustomEventData>
{ }

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> events;
    private Dictionary<string, CustomEvent> typedEvents;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                else
                    eventManager.Init();
            }

            return eventManager;
        }
    }

    private void Init()
    {
        if (events == null)
        {
            events = new Dictionary<string, UnityEvent>();
        }

        if (typedEvents == null)
        {
            typedEvents = new Dictionary<string, CustomEvent>();
        }
    }

    public static void AddListener(string eventName, UnityAction listener)
    {
        UnityEvent evt = null;
        if (instance.events.TryGetValue(eventName, out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new UnityEvent();
            evt.AddListener(listener);
            instance.events.Add(eventName, evt);
        }
    }

    public static void RemoveListener(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent evt = null;
        if (instance.events.TryGetValue(eventName, out evt))
            evt.RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent evt = null;
        if (instance.events.TryGetValue(eventName, out evt))
            evt.Invoke();
    }

    public static void AddTypedListener(string eventName, UnityAction<CustomEventData> listener)
    {
        CustomEvent evt = null;
        if (instance.typedEvents.TryGetValue(eventName, out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new CustomEvent();
            evt.AddListener(listener);
            instance.typedEvents.Add(eventName, evt);
        }
    }

    public static void RemoveTypedListener(string eventName, UnityAction<CustomEventData> listener)
    {
        if (eventManager == null) return;
        CustomEvent evt = null;
        if (instance.typedEvents.TryGetValue(eventName, out evt))
            evt.RemoveListener(listener);
    }

    public static void TriggerTypedEvent(string eventName, CustomEventData data)
    {
        CustomEvent evt = null;
        if (instance.typedEvents.TryGetValue(eventName, out evt))
            evt.Invoke(data);
    }
}