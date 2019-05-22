using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;

[RequireComponent(typeof(DelayedCallService))]
public class ElevatorComponent : EXBehaviour, IElevatorComponent
{
    private string componentName;
    public FloorItem CurrentFloor { get { return Owner.CurentFloorItem; } }
    public Elevator Owner { get; set; }
    public DelayedCallService DelayedCallService { get; set; }
    protected string Name { get => componentName; set => componentName = value; }
    protected bool IsDoorIdle { get => Owner.IsDoorClosed; }
    public virtual void DeInit()
    {
        this.EXEventStopListening<ElevatorEvent>();
    }
    public virtual void Init(Elevator owner)
    {
        this.EXEventStartListening<ElevatorEvent>();
        Owner = owner;
        DelayedCallService = gameObject.GetComponent<DelayedCallService>();
    }
    public virtual void OnEXEvent(ElevatorEvent eventType)
    {
        this.EXEventStopListening<ElevatorEvent>();
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
    }
    protected void SetDebugMsg(string msg)
    {
        ElevatorDebugText.Instance.SetText(Name, msg);
    }
}
