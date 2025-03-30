using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class UIView : MonoBehaviour {
        [SerializeField] List<UIViewButton> _buttons;
        
        [SerializeField] List<UIViewSlider> _sliders;
        [SerializeField] List<UIViewDropdown> _dropdowns;


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
            _sliders.ForEach(x => x.sliderReference.onValueChanged.AddListener(delegate {
                EventBus<UISliderChanged>.Raise(new UISliderChanged {
                    sliderType = x.sliderType,
                    value = x.sliderReference.value,
                });
            }));
            
            _dropdowns.ForEach(x => x.dropdownReference.onValueChanged.AddListener(delegate {
                EventBus<UIDropdownChanged>.Raise(new UIDropdownChanged {
                    dropdownType = x.dropdownType,
                    value = x.dropdownReference.value + 1,
                });
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