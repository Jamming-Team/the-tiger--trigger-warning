using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static Tiger.DataSO;

namespace Tiger {
    public class AudioManager : MonoBehaviour, IVisitable {
        const string MUSIC_VOLUME_NAME = "MusicVolume";
        const string SFX_VOLUME_NAME = "SfxVolume";
        
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] AudioSource[] _musicSources = new AudioSource[2];

        [HideInInspector]
        public AudioData data { get; set; }
        
        MusicManager _music;
        
        void Start() {
            GameManager.Instance.RequestData(this);

            AdjustMixerVolume();
            
            _music = new MusicManager(data.music, new MusicManager.MusicSourcesPair
            {
                sourceOne = _musicSources[0],
                sourceTwo = _musicSources[1]
            });
            
            _music.LoadBundle(MusicBundleType.MainMenu);
            _music.PlayNextTrack();
        }

        void Update() {
            _music.CheckForCrossFade();
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }

        void AdjustMixerVolume() {
            _mixer.SetFloat(SFX_VOLUME_NAME, data.sfxVolume.ToLogarithmicVolume());
            _mixer.SetFloat(MUSIC_VOLUME_NAME, data.musicVolume.ToLogarithmicVolume());
        }
    }
}