using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public abstract class SceneState<TContextType> : State<TContextType> where TContextType : MonoBehaviour {
        [SerializeField] protected List<GameObject> _views;
        EventBinding<UIButtonPressed> _UIEventBinding;

        public override void Init(MonoBehaviour context) {
            base.Init(context);
            SetViewsVisibility(false);
        }

        protected override void OnEnter() {
            SetViewsVisibility(true);
            _UIEventBinding = new EventBinding<UIButtonPressed>(OnUIButtonPressed);
            EventBus<UIButtonPressed>.Register(_UIEventBinding);
        }

        protected override void OnExit() {
            SetViewsVisibility(false);
            EventBus<UIButtonPressed>.Deregister(_UIEventBinding);
        }

        void SetViewsVisibility(bool visibility) {
            _views?.ForEach(x => {
                // Debug.Log(x.gameObject.name);
                if (x)
                    x.SetActive(visibility);
            });
        }
        
        protected virtual void OnUIButtonPressed(UIButtonPressed e) {}
    }
}