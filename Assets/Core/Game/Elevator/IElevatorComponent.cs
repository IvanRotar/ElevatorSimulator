using EX.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElevatorComponent : EXEventListener<ElevatorEvent>
{
    Elevator Owner { get; set; }
    DelayedCallService DelayedCallService { get; set; }
    void Init(Elevator owner);
    void DeInit();
}
