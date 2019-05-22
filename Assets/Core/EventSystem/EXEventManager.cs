using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public static class EXEventManager
{
    private static Dictionary<Type, List<EXEventListenerBase>> _subscribersList;
    static EXEventManager()
    {
        _subscribersList = new Dictionary<Type, List<EXEventListenerBase>>();
    }
    public static void AddListener<EXEvent>(EXEventListener<EXEvent> listener) where EXEvent : struct
    {
        Type eventType = typeof(EXEvent);

        if (!_subscribersList.ContainsKey(eventType))
            _subscribersList[eventType] = new List<EXEventListenerBase>();

        if (!SubscriptionExists(eventType, listener))
            _subscribersList[eventType].Add(listener);
    }
    public static void RemoveListener<MMEvent>(EXEventListener<MMEvent> listener) where MMEvent : struct
    {
        Type eventType = typeof(MMEvent);
        if (!_subscribersList.ContainsKey(eventType))
        {
            return;
        }
        List<EXEventListenerBase> subscriberList = _subscribersList[eventType];
        bool listenerFound;
        listenerFound = false;
        if (listenerFound)
        {

        }
        for (int i = 0; i < subscriberList.Count; i++)
        {
            if (subscriberList[i] == listener)
            {
                subscriberList.Remove(subscriberList[i]);
                listenerFound = true;

                if (subscriberList.Count == 0)
                    _subscribersList.Remove(eventType);

                return;
            }
        }
    }
    public static void TriggerEvent<EXEvent>(EXEvent newEvent) where EXEvent : struct
    {
        List<EXEventListenerBase> list;
        if (!_subscribersList.TryGetValue(typeof(EXEvent), out list))
            return;

        for (int i = 0; i < list.Count; i++)
        {
            (list[i] as EXEventListener<EXEvent>).OnEXEvent(newEvent);
        }
    }
    private static bool SubscriptionExists(Type type, EXEventListenerBase receiver)
    {
        List<EXEventListenerBase> receivers;

        if (!_subscribersList.TryGetValue(type, out receivers)) return false;

        bool exists = false;

        for (int i = 0; i < receivers.Count; i++)
        {
            if (receivers[i] == receiver)
            {
                exists = true;
                break;
            }
        }

        return exists;
    }
}
public static class EventRegister
{
    public delegate void Delegate<T>(T eventType);

    public static void EXEventStartListening<EventType>(this EXEventListener<EventType> caller) where EventType : struct
    {
        EXEventManager.AddListener<EventType>(caller);
    }

    public static void EXEventStopListening<EventType>(this EXEventListener<EventType> caller) where EventType : struct
    {
        EXEventManager.RemoveListener<EventType>(caller);
    }
}
#region EXEventListenerBase
public interface EXEventListenerBase { };
public interface EXEventListener<T> : EXEventListenerBase
{
    void OnEXEvent(T eventType);
}

#endregion