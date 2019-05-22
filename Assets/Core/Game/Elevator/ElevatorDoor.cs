using System.Collections;
using System.Collections.Generic;
using EX.StateMachine;
using EX.Tools;
using UnityEngine;

public enum DoorState
{
    Idle, Open, Close
}
public class ElevatorDoor : ElevatorComponent
{
    [SerializeField] private Transform door;
    private Animator animator;
    public override void Init(Elevator owner)
    {
        base.Init(owner);
        animator = door.GetComponent<Animator>();
        ElevatorHelper.ElevatorDoor = door;
        Owner.DoorState = DoorState.Idle;
        Name = "ElevatorDoor";
    }
    void OpenDoor()
    {
        TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.OpenDoor, CurrentFloor));
        animator.SetBool(DoorAnimationHash.OpenHash, true);
        DelayedCallService.DelayedCall(animator.GetCurrentAnimatorClipInfo(0).Length + 2, () =>
         {
             Owner.DoorState = DoorState.Open;
             SetDebugMsg("Open door");
             DelayedCallService.DelayedCall(GameDataHandler.ElevatorDoorIdle, () =>
             {
                 Owner.DoorState = DoorState.Close;
                 CloseDoor();
             });
         });
    }
    void CloseDoor()
    {
        animator.SetBool(DoorAnimationHash.OpenHash, false);
        DelayedCallService.DelayedCall(animator.GetCurrentAnimatorClipInfo(0).Length + 2, () =>
        {
            EndCloseDoorAnimation();
        });
    }
    private void EndCloseDoorAnimation()
    {
        SetDebugMsg("Close door");
        Owner.DoorState = DoorState.Idle;
        TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.CloseDoor, CurrentFloor));
    }
    [ContextMenu("Force")]
    private void ForceDoorClose()
    {
        animator.SetBool(DoorAnimationHash.OpenHash, false);
        animator.Play(DoorAnimationHash.IdleState, 0, 0);
        EndCloseDoorAnimation();
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
    }
    public override void OnEXEvent(ElevatorEvent eventType)
    {
        switch (eventType.elevatorEventType)
        {
            case ElevatorEvent.ElevatorEventType.OnTheFloor:
                OpenDoor();
                break;
            case ElevatorEvent.ElevatorEventType.ForceStop:
                CloseDoor();
                break;
            case ElevatorEvent.ElevatorEventType.ForceDoorClose:
                if (Owner.DoorState == DoorState.Open)
                    ForceDoorClose();
                break;
        }
    }
}
