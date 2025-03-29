using System;
using System.Reflection;
using UnityEngine;
using static Tiger.DataSO;

namespace Tiger {
    public class DataManager : IVisitor {
        DataSO _dataSO;

        EventBinding<UISliderChanged> _UiSliderChangedBinding;

        public DataManager(DataSO dataSO) {
            _dataSO = dataSO;
            _UiSliderChangedBinding = new EventBinding<UISliderChanged>(UiSliderChanged);
            EventBus<UISliderChanged>.Register(_UiSliderChangedBinding);
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
    }
}