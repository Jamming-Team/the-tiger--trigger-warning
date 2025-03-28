using UnityEngine;

namespace Tiger {
    public class GP_SceneController : SceneController, IVisitable {
        [HideInInspector]
        public DataSO.GameData data { get; set; }

        void Start() {
            GameManager.Instance.RequestData(this);
            Debug.Log(data.test);
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }


    }
}