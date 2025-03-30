using System;
using System.Reflection;
using UnityEngine;
using static Tiger.DataSO;

namespace Tiger {
    public class DataManager : IVisitor {
        DataSO _dataSO;

        EventBinding<UISliderChanged> _UiSliderChangedBinding;
        
        EventBinding<UIDropdownChanged> dropdownChangedBinding;

        public DataManager(DataSO dataSO) {
            _dataSO = dataSO;
            _UiSliderChangedBinding = new EventBinding<UISliderChanged>(UiSliderChanged);
            EventBus<UISliderChanged>.Register(_UiSliderChangedBinding);
            
            
            dropdownChangedBinding = new EventBinding<UIDropdownChanged>(UiDropdownChanged);
            EventBus<UIDropdownChanged>.Register(dropdownChangedBinding);
            

        }

        void UiSliderChanged(UISliderChanged evt) {
            switch (evt.sliderType) {
                case UISliders.MusicVolume: {
                    _dataSO.audio.musicVolume = evt.value;
                    break;
                }
                case UISliders.SfxVolume: {
                    _dataSO.audio.sfxVolume = evt.value;
                    break;
                }
            }
            EventBus<DataChanged>.Raise(new DataChanged());
        }

        void UiDropdownChanged(UIDropdownChanged evt) {
            switch (evt.dropdownType) {
                case UIDropdownTypes.InitialObj: {
                    _dataSO.game.initialObjectsCount = evt.value;
                    break;
                }
                case UIDropdownTypes.MaxObj: {
                    _dataSO.game.maxObjectsCount = evt.value + 4;
                    break;
                }
                case UIDropdownTypes.LivesCount: {
                    _dataSO.game.livesCount = evt.value - 1;
                    break;
                }
                case UIDropdownTypes.TilIncrease: {
                    _dataSO.game.turnsTillIncrease = evt.value;
                    break;
                }
            }
        }

        public void TrySupply(IVisitable requester) {
            requester.Accept(this);
        }

        // --- Suppliers --- 

        public void Visit(object o) {
            var visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { typeof(object) }))
                visitMethod.Invoke(this, new object[] { o });
            else
                DefaultVisit(o);
        }

        void DefaultVisit(object o) {
            // noop (== `no op` == `no operation`)
            Debug.Log("MCDataFillerVisitor.DefaultVisit");
        }

        public void Visit(GP_SceneController requester) {
            requester.data = _dataSO.game;
        }

        public void Visit(AudioManager requester) {
            requester.data = _dataSO.audio;
        }
        
        public void Visit(CameraController requester) {
            requester.data = _dataSO.game.camera;
        }
        
        public void Visit(NotifierView requester) {
            requester.data = _dataSO.ui;
        }
        
        public void Visit(UIViewSlider requester) {
            requester.data = _dataSO.audio;
        }
        
        public void Visit(UIViewDropdown requester) {
            requester.data = _dataSO.game;
        }
    }
}