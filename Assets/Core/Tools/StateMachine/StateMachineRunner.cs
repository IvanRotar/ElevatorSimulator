using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = System.Object;

namespace EX.StateMachine
{
    public class StateMachineRunner : MonoBehaviour
    {
        private List<IStateMachine> stateMachineList = new List<IStateMachine>();

        public StateMachine<T> Initialize<T>(MonoBehaviour component) where T : struct, IConvertible, IComparable
        {
            var fsm = new StateMachine<T>(this, component);

            stateMachineList.Add(fsm);

            return fsm;
        }
        public StateMachine<T> Initialize<T>(MonoBehaviour component, T startState)
            where T : struct, IConvertible, IComparable
        {
            var fsm = Initialize<T>(component);

            fsm.ChangeState(startState);

            return fsm;
        }

        void FixedUpdate()
        {

            for (int i = 0; i < stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.FixedUpdate();
                }
            }
        }

        void Update()
        {
            for (int i = 0; i < stateMachineList.Count; i++)
            {

                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.Update();
                }
            }
        }

        void LateUpdate()
        {
            for (int i = 0; i < stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];

                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.LateUpdate();
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            for (int i = 0; i < stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.OnCollisionEnter(collision);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {

            for (int i = 0; i < stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.OnTriggerEnter(other);
                }
            }

        }

        void OnTriggerStay(Collider other)
        {

            for (int i = 0; i < stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.OnTriggerStay(other);
                }
            }

        }

        void OnTriggerExit(Collider other)
        {

            for (int i = 0; i < stateMachineList.Count; i++)
            {
                var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.OnTriggerExit(other);
                }
            }

        }

        public static void DoNothing()
        {
        }

        public static void DoNothingCollider(Collider other)
        {
        }

        public static void DoNothingCollision(Collision other)
        {
        }

        public static void DoNothingTrigger(Collider other)
        {
        }

        public static IEnumerator DoNothingCoroutine()
        {
            yield break;
        }
    }


    public class StateMapping
    {
        public object state;

        public bool hasEnterRoutine;
        public Action EnterCall = StateMachineRunner.DoNothing;
        public Func<IEnumerator> EnterRoutine = StateMachineRunner.DoNothingCoroutine;

        public bool hasExitRoutine;
        public Action ExitCall = StateMachineRunner.DoNothing;
        public Func<IEnumerator> ExitRoutine = StateMachineRunner.DoNothingCoroutine;

        public Action Finally = StateMachineRunner.DoNothing;
        public Action Update = StateMachineRunner.DoNothing;
        public Action LateUpdate = StateMachineRunner.DoNothing;
        public Action FixedUpdate = StateMachineRunner.DoNothing;
        public Action<Collision> OnCollisionEnter = StateMachineRunner.DoNothingCollision;
        public Action<Collider> OnTriggerEnter = StateMachineRunner.DoNothingTrigger;
        public Action<Collider> OnTriggerStay = StateMachineRunner.DoNothingTrigger;
        public Action<Collider> OnTriggerExit = StateMachineRunner.DoNothingTrigger;
        public StateMapping(object state)
        {
            this.state = state;
        }
    }
}
