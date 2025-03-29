using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class SceneController : MonoBehaviour {
        [SerializeField] protected StateMachine _stateMachine;

        protected virtual void Start() {
            _stateMachine.Init(this);
        }
    }
}