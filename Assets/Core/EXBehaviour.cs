using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;

public abstract class EXBehaviour : MonoBehaviour,
                                    EXEventListener<GameMainEvent>
{
    protected void TriggerEvent<EXEvent>(EXEvent newEvent) where EXEvent : struct
    {
        EXEventManager.TriggerEvent(newEvent);
    }
    protected void OnEnable()
    {
        this.EXEventStartListening<GameMainEvent>();
        Show();
    }
    protected void OnDisable()
    {
        this.EXEventStopListening<GameMainEvent>();
        Hide();
    }
    protected virtual void Show()
    {

    }
    protected virtual void Hide()
    {

    }
    public abstract void OnEXEvent(GameMainEvent eventType);
}
