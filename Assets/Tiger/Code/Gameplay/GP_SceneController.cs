using UnityEngine;

namespace Tiger {
    public class GP_SceneController : SceneController, IVisitable {
        [HideInInspector]
        public DataSO.GameData data { get; set; }

        public bool freeActIsInAction => _stateMachine.currentState is Gameplay.GP_ActionState;
        
        protected override void Start() {

            GameManager.Instance.RequestData(this);
            Debug.Log(data.test);
            
            _stateMachine.Init(this);

        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }


    }
}