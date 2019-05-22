using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;
using EX.StateMachine;
using DigitalRuby.Tween;

public class ElevatorMotor : ElevatorComponent
{
    private enum MotorState
    {
        Idle, Move, Wait,Stop
    }
    private StateMachine<MotorState> stateMachine;
    public List<int> MoveTarget = new List<int>();
    public bool HasTarget
    {
        get
        {
            return MoveTarget.Count > 0;
        }
    }
    #region Api
    public bool IsMove
    {
        get
        {
            return stateMachine.State != MotorState.Idle;
        }
    }
    public void AddFloor(int pos)
    {
        MoveTarget.Add(pos);
        ElevatorHelper.SortFloor(ref MoveTarget, Owner.ElevatorDirection);
        if (!IsMove) stateMachine.ChangeState(MotorState.Wait);
    }
    public void RemoveLastFloor()
    {
        MoveTarget.RemoveAt(0);
        ElevatorHelper.SortFloor(ref MoveTarget, Owner.ElevatorDirection);
    }
    #endregion
    #region StateMachine
    void Wait_Update()
    {
        if (HasTarget)
        {
            if (IsDoorIdle)
                stateMachine.ChangeState(MotorState.Move);
        }
        else stateMachine.ChangeState(MotorState.Idle);
    }
    void Move_Enter()
    {
        SetDebugMsg("Start move");
        System.Action<ITween<Vector3>> OnMoveLift = (t) =>
        {
            SetDebugMsg("On Move");
            ElevatorHelper.ElevatorDoor.transform.position = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> MoveLiftCompleted = (t) =>
        {
            SetDebugMsg("End move");
            TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.OnTheFloor, FloorManager.Instance.GetFloorByNumber(MoveTarget[0])));
            stateMachine.ChangeState(MotorState.Idle);
            RemoveLastFloor();
        };
        ElevatorHelper.ElevatorDoor.gameObject.Tween("LifMove", ElevatorHelper.ElevatorDoor.transform.position, FloorManager.Instance.GetFloorByNumber(MoveTarget[0]).LiftMovePose, 1.75f,
            TweenScaleFunctions.CubicEaseIn, OnMoveLift, MoveLiftCompleted); 
    }
    void Stop_Enter()
    {
        DelayedCallService.RemoveAllDelayed();
        MoveTarget = new List<int>();
        stateMachine.ChangeState(MotorState.Idle);
    }
    #endregion
    #region Init
    public override void Init(Elevator owner)
    {
        base.Init(owner);
        Name = "ElevatorMotor";
        stateMachine = StateMachine<MotorState>.Initialize(this, MotorState.Idle);
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
    }
    #endregion
    #region Event
    public override void OnEXEvent(ElevatorEvent eventType)
    {
        switch (eventType.elevatorEventType)
        {
            case ElevatorEvent.ElevatorEventType.CloseDoor:
                stateMachine.ChangeState(MotorState.Wait);
                break;
            case ElevatorEvent.ElevatorEventType.ForceStop:
                stateMachine.ChangeState(MotorState.Stop);
                break;
        }
    }
    #endregion
}

