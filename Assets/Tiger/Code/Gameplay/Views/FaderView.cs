using System;
using UnityEngine;

namespace Tiger.Views {
    public class FaderView : MonoBehaviour {
        [SerializeField] Animator _animator;

        EventBinding<FadeRequest> _fadeRequestBinding;

        void Start() {
            _fadeRequestBinding = new EventBinding<FadeRequest>(PerformFadeAction);
            EventBus<FadeRequest>.Register(_fadeRequestBinding);
        }

        void PerformFadeAction(FadeRequest fadeRequest) {
            if (fadeRequest.shouldFade)
                _animator.SetTrigger("FadeIn");
            else
                _animator.SetTrigger("FadeOut");
        }
    }
}