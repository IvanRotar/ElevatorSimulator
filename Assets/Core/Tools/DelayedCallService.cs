using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public class DelayedCallService : MonoBehaviour, IDelayedCallService
    {
        public int DalayedCallsCount;

        private List<DelayedCallbackItem> delayedCalls;

        void Awake()
        {
            delayedCalls = new List<DelayedCallbackItem>();
        }

        void Update()
        {
            Action dCall;
            for (int i = 0; i < delayedCalls.Count; i++)
            {
                if (delayedCalls[i].Delay <= GetTime(delayedCalls[i].useRealTime))
                {
                    dCall = delayedCalls[i].DelCallback;

                    if (delayedCalls.Count > i)
                    {
                        delayedCalls.RemoveAt(i);
                        i--;
                    }

                    dCall();
                }
            }

            DalayedCallsCount = delayedCalls.Count;
        }

        protected virtual float GetTime(bool useRealTime)
        {
            return useRealTime ? Time.realtimeSinceStartup : Time.time;
        }

        public void DelayedCall(float delay, Action callback, bool useRealTime = true)
        {
            delayedCalls.Add(new DelayedCallbackItem(delay + GetTime(useRealTime), callback, useRealTime));
            DalayedCallsCount = delayedCalls.Count;
        }

        public void RemoveDelayedCallsTo(Action callback)
        {
            for (int i = 0; i < delayedCalls.Count; i++)
            {
                if (delayedCalls[i].DelCallback == callback)
                {
                    delayedCalls.RemoveAt(i);
                    i--;
                }
            }
        }
        public void RemoveAllDelayed()
        {
            delayedCalls.Clear();
        }
        class DelayedCallbackItem
        {
            public float Delay;
            public Action DelCallback;

            public bool useRealTime = true;

            public DelayedCallbackItem(float delay, Action callback, bool useRealTime = true)
            {
                Delay = delay;
                DelCallback = callback;
                this.useRealTime = useRealTime;
            }

        }
    }
}