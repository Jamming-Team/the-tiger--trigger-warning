using System;
using UnityEngine;

namespace Tiger {
    public class FaderView : MonoBehaviour {
        [SerializeField] Animator _animator;

        EventBinding<FadeRequest> _fadeRequestBinding;

        void Start() {
            _fadeRequestBinding = new EventBinding<FadeRequest>(PerformFadeAction);
            EventBus<FadeRequest>.Register(_fadeRequestBinding);
            
            _animator.gameObject.SetActive(true);
        }

        void OnDestroy() {
                EventBus<FadeRequest>.Deregister(_fadeRequestBinding);
        }

        void PerformFadeAction(FadeRequest fadeRequest) {
            
            Debug.Log($"FaderView::PerformFadeAction::{fadeRequest.shouldFade}");
            if (fadeRequest.shouldFade)
                _animator.SetTrigger("FadeIn");
            else
                _animator.SetTrigger("FadeOut");
        }
    }
}