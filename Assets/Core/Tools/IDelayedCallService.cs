using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public interface IDelayedCallService
    {
        void DelayedCall(float delay, Action callback, bool useRealTime = true);  
        void RemoveDelayedCallsTo(Action callback);
    }
}