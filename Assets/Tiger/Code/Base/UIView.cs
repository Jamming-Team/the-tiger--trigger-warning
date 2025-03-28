using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class UIView : MonoBehaviour {
        [SerializeField] List<UIViewButton> _buttons;

        EventBinding<UIButtonPressed> testEventBinding;

        // void Start() {
        //     _buttons.ForEach(x => x.buttonReference.onClick.AddListener(delegate {
        //         RaiseUIButtonEvent(x.buttonType);
        //     }));
        //     
        //     testEventBinding = new EventBinding<UIButtonPressed>(Test);
        //     EventBus<UIButtonPressed>.Register(testEventBinding);
        // }

        void Start() {
            _buttons.ForEach(x => x.buttonReference.onClick.AddListener(delegate {
                RaiseUIButtonEvent(x.buttonType);
            }));
            
            testEventBinding = new EventBinding<UIButtonPressed>(Test);
            EventBus<UIButtonPressed>.Register(testEventBinding);
        }


        void OnDestroy() {
            _buttons.ForEach(x => x.buttonReference.onClick.RemoveListener(delegate {
                RaiseUIButtonEvent(x.buttonType);
            }));
            
            EventBus<UIButtonPressed>.Deregister(testEventBinding);
        }

        private void Test(UIButtonPressed data) {
            Debug.Log($"UIViewButton: {data.buttonType}");
        }

        private void RaiseUIButtonEvent(UIButtonTypes buttonType) {
            Debug.Log($"UIViewButton TRUE: {buttonType}");
            EventBus<UIButtonPressed>.Raise(new UIButtonPressed {
                buttonType = buttonType,
            });
        }
    }
}