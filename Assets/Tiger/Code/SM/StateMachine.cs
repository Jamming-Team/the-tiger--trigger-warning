using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class StateMachine : MonoBehaviour {
        readonly List<IState> _states = new();
        public IState currentState { get; private set; }


        public void Init(MonoBehaviour core) {
            GetComponentsInChildren(_states);
            _states.ForEach(x => {
                x.OnTransitionRequired += ChangeState;
                x.Init(core);
            });
            ChangeState(_states[0].GetType());
        }

        public void OnDestroy() {
            _states.ForEach(x => { x.OnTransitionRequired -= ChangeState; });
        }

        void ChangeState(Type nextStateType) {
            var nextState = _states.Find(x => x.GetType() == nextStateType);

            // Debug.Log(nextState);

            // Debug.Log(!Equals(m_currentState, nextState));


            if (nextState != null && !Equals(currentState, nextState)) {
                if (currentState != null) currentState.Exit();
                nextState.Enter();
                currentState = nextState;
                // Debug.Log(m_currentState);
            }
        }
    }
}