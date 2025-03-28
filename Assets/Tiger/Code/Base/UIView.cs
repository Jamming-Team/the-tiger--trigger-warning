using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class UIView : MonoBehaviour {
        [SerializeField] List<UIViewButton> _buttons = new();

        EventBinding<UIButtonPressed> testEventBinding;

        void OnEnable() {
            _buttons.ForEach(x => x.buttonReference.onClick.AddListener(delegate {
                PressUIButton(x.buttonType);
            }));
        }

        void Start() {
            testEventBinding = new EventBinding<UIButtonPressed>(Test);
            EventBus<UIButtonPressed>.Register(testEventBinding);
        }

        void OnDisable() {
            EventBus<UIButtonPressed>.Deregister(testEventBinding);
        }

        private void Test(UIButtonPressed data) {
            Debug.Log($"UIViewButton: {data.buttonType}");
        }

        public void PressUIButton(UIButtonTypes buttonType) {
            EventBus<UIButtonPressed>.Raise(new UIButtonPressed {
                buttonType = buttonType,
            });
        }
    }
}