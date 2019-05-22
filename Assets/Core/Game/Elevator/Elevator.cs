
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;
using EX.StateMachine;


[RequireComponent(typeof(ElevatorMotor))]
public class Elevator : EXBehaviour, EXEventListener<ElevatorEvent>
{
    [SerializeField] protected ElevatorCondition elevatorCondition;
    protected ElevatorMotor elevatorMotor;
    protected IElevatorComponent[] elevatorComponents;
    private DoorState doorState;
    #region Event
    protected override void Show()
    {
        elevatorMotor = GetComponent<ElevatorMotor>();
        this.EXEventStartListening<ElevatorEvent>();
        elevatorComponents = GetComponents<IElevatorComponent>();
        foreach (IElevatorComponent obj in elevatorComponents) obj.Init(this);
    }
    protected override void Hide()
    {
        this.EXEventStopListening<ElevatorEvent>();
        elevatorMotor.DeInit();
    }
    public void OnEXEvent(ElevatorEvent eventType)
    {
        switch (eventType.elevatorEventType)
        {
            case ElevatorEvent.ElevatorEventType.Move:
                MoveInit(eventType.floorItem, eventType.elevatorMoveDirection);
                break;
            case ElevatorEvent.ElevatorEventType.LiftCabineMoveComand:
                MoveInit(eventType.floorItem, ElevatorDirection);
                break;
            case ElevatorEvent.ElevatorEventType.OnTheFloor:
                CurentFloorItem = eventType.floorItem;
                break;
        }
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
        switch (eventType.gameMainEventType)
        {
            case GameMainEvent.GameMainEventType.FloorInit:
                SetFloor(FloorManager.Instance.GetFloorByNumber(1), ElevatorMoveDirection.Up);
                break;
        }
    }
    #endregion
    #region Api
    private bool IsCurrentFloor(FloorItem floorItem)
    {
        return floorItem == CurentFloorItem;
    }
    public ElevatorMoveDirection ElevatorDirection
    {
        get
        {
            return elevatorCondition.elevatorMoveDirection;
        }
        set
        {
            elevatorCondition.elevatorMoveDirection = value;
        }
    }
    public DoorState DoorState { get => doorState; set => doorState = value; }
    public FloorItem CurentFloorItem { get => curentFloorItem; set => curentFloorItem = value; }
    public bool IsDoorClosed { get => DoorState == DoorState.Idle; }

    private FloorItem curentFloorItem;
    #endregion
    private void MoveInit(FloorItem floorItem, ElevatorMoveDirection elevatorMoveDirection)
    {
        if (!elevatorMotor.HasTarget)
        {
            ElevatorDirection = ElevatorHelper.CalculateDirection(CurentFloorItem,floorItem);
        }
        else
        {
            if (!ElevatorHelper.IdenticalDirection(CurentFloorItem, floorItem, ElevatorDirection, elevatorMoveDirection))
                return;
        }
        if (IsCurrentFloor(floorItem))
        {
            TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.OnTheFloor, floorItem));
            return;
        }
        elevatorMotor.AddFloor(floorItem.FloorCell.FloorPosition);
    }
    private void SetFloor(FloorItem floorItem, ElevatorMoveDirection elevatorMoveDirection)
    {
        elevatorCondition = new ElevatorCondition();
        TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.OnTheFloor, floorItem));
    }
}
