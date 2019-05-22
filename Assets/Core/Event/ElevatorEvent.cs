using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public struct ElevatorEvent
    {
        public enum ElevatorEventType
        {
            Move,Stop,OpenDoor,CloseDoor,OnTheFloor,ForceStop,ForceDoorClose,LiftCabineMoveComand
        }
        public ElevatorEventType elevatorEventType;
        public FloorItem floorItem;
        public ElevatorMoveDirection elevatorMoveDirection;
        public ElevatorEvent(ElevatorEventType elevatorEventType, FloorItem floorItem, ElevatorMoveDirection elevatorMoveDirection)
        {
            this.elevatorEventType = elevatorEventType;
            this.floorItem = floorItem;
            this.elevatorMoveDirection = elevatorMoveDirection;
        }
        public ElevatorEvent(ElevatorEventType elevatorEventType, FloorItem floorItem)
        {
            this.elevatorEventType = elevatorEventType;
            this.floorItem = floorItem;
            this.elevatorMoveDirection = ElevatorMoveDirection.Up;
        }
        public ElevatorEvent(ElevatorEventType elevatorEventType)
        {
            this.elevatorEventType = elevatorEventType;
            this.floorItem = null;
            this.elevatorMoveDirection = ElevatorMoveDirection.Up;
        }
    }
}