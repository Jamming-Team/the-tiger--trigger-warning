using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static Tiger.DataSO;

namespace Tiger {
    public class AudioManager : Singleton<AudioManager>, IVisitable {
        const string MUSIC_VOLUME_NAME = "MusicVolume";
        const string SFX_VOLUME_NAME = "SfxVolume";
        
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] AudioSource[] _musicSources = new AudioSource[2];
        [SerializeField] SoundData _testSound;
        [SerializeField] SoundManager _soundModel;


        [HideInInspector]
        public AudioData data { get; set; }
        
        MusicManager _music;
        
        EventBinding<DataChanged> _DataChangedBinding;

        
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
            
            _DataChangedBinding = new EventBinding<DataChanged>(AdjustMixerVolume);
            EventBus<DataChanged>.Register(_DataChangedBinding);
        }

        void Update() {
            _music.CheckForCrossFade();

            if (Input.GetKeyDown(KeyCode.Space)) {
                PlaySound(_testSound);
            }
        }

        public void PlaySound(SoundData soundData, Transform playTransform = null) {
            var a = _soundModel.CreateSoundBuilder()
                .WithRandomPitch();
            if (playTransform != null) {
                a.WithPosition(playTransform.position);
            } 
            a.Play(soundData);
        }
        
        void AdjustMixerVolume() {
            _mixer.SetFloat(SFX_VOLUME_NAME, data.sfxVolume.ToLogarithmicVolume());
            _mixer.SetFloat(MUSIC_VOLUME_NAME, data.musicVolume.ToLogarithmicVolume());
        }
        

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }


    }
}