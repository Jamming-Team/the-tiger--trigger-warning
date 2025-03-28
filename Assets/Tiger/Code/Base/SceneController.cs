using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class SceneController : MonoBehaviour {
        [SerializeField] StateMachine _stateMachine;

        void Start() {
            _stateMachine.Init(this);
        }
    }
}