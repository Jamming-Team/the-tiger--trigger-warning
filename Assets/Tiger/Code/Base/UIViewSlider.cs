using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tiger {
    [RequireComponent(typeof(Slider))]
    public class UIViewSlider : MonoBehaviour, IVisitable {
        public UISliders sliderType;
        [HideInInspector] public Slider sliderReference;
        
        [HideInInspector] public DataSO.AudioData data;

        void Start() {
            sliderReference = GetComponent<Slider>();
            
            GameManager.Instance.RequestData(this);

            switch (sliderType) {
                case UISliders.MusicVolume: {
                    sliderReference.value = data.musicVolume;
                    break;
                }
                case UISliders.SfxVolume: {
                    sliderReference.value = data.sfxVolume;
                    break;
                }
            }
        }
        
        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}