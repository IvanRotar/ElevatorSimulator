using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ElevatorMoveEvent 
{
    public FloorItem floorItem;
    public ElevatorMoveEvent(FloorItem floorItem)
    {
        this.floorItem = floorItem;
    }
}
