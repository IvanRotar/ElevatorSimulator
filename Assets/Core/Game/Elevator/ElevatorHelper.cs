
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ElevatorHelper
{
    public static Transform ElevatorDoor;
    public static void SortFloor(ref List<int> Floor,ElevatorMoveDirection elevatorMoveDirection)
    {
        Floor = Floor.Distinct().ToList();
        switch (elevatorMoveDirection)
        {
            case ElevatorMoveDirection.Up:
                Floor= Floor.OrderBy(val => val).ToList();
                break;
            case ElevatorMoveDirection.Down:
                Floor = Floor.OrderByDescending(val => val).ToList();
                break;
        }
    }
    public static ElevatorMoveDirection CalculateDirection(FloorItem CurrentFloor,FloorItem NewFloor)
    {
        return CurrentFloor.FloorNumber > NewFloor.FloorNumber ? ElevatorMoveDirection.Down : ElevatorMoveDirection.Up;
    }
    public static bool IdenticalDirection(FloorItem LastFloorPosition, FloorItem NewPosition, ElevatorMoveDirection elevatorMoveDirection,ElevatorMoveDirection NewDirection)
    {
        bool ret = false;
        switch (elevatorMoveDirection)
        {
            case ElevatorMoveDirection.Up:
                ret = LastFloorPosition.FloorNumber < NewPosition.FloorNumber && elevatorMoveDirection == NewDirection;
                break;
            case ElevatorMoveDirection.Down:
                ret = LastFloorPosition.FloorNumber > NewPosition.FloorNumber && elevatorMoveDirection == NewDirection;
                break;
        }
        return ret;
    }
}
