using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoorAnimationHash
{
    public static readonly int OpenHash;
    public static readonly int IdleState;
    static DoorAnimationHash()
    {
        OpenHash = Animator.StringToHash("Open");
        IdleState = Animator.StringToHash("Idle");
    }
}
