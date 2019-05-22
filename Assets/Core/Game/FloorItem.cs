using System.Collections;
using System.Collections.Generic;
using EX.Tools;
using UnityEngine;

public class FloorItem : EXBehaviour, IInitable, EXEventListener<ElevatorEvent>
{
    [SerializeField] private FloorCell floorCell;
    public FloorCell FloorCell { get => floorCell; }
    public int FloorNumber { get => floorCell.FloorPosition; set => floorCell.FloorPosition = value; }
    public Vector3 LiftMovePose
    {
        get
        {
            return floorCell.LiftPointPosition();
        }
    }
    public void DeInit()
    {
        this.EXEventStopListening<ElevatorEvent>();
        TriggerEvent(new FloorEvent(FloorEvent.FloorEventtype.Delete, this));
    }
    public void Init()
    {
        this.EXEventStartListening<ElevatorEvent>();
        TriggerEvent(new FloorEvent(FloorEvent.FloorEventtype.Create, this));
        ControllButtonInit();
    }
    private void ControllButtonInit()
    {
        FloorCell.GetFloorControllButton(ElevatorMoveDirection.Up).onClick.AddListener(() => ButtonComand(ElevatorMoveDirection.Up));
        FloorCell.GetFloorControllButton(ElevatorMoveDirection.Down).onClick.AddListener(() => ButtonComand(ElevatorMoveDirection.Down));
    }
    public void OnCreate(int PosY)
    {
        FloorCell.Init(PosY);
    }
    private void ButtonComand(ElevatorMoveDirection elevatorMoveDirection)
    {
        TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.Move, this, elevatorMoveDirection));
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
        
    }
    public void OnEXEvent(ElevatorEvent eventType)
    {
        switch (eventType.elevatorEventType)
        {
            case ElevatorEvent.ElevatorEventType.OpenDoor:
                if (eventType.floorItem == this)
                {
                    ElevatorHelper.ElevatorDoor.SetParent(transform);
                    ElevatorHelper.ElevatorDoor.transform.position = LiftMovePose;
                }
                break;
        }
    }
}
