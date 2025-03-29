using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Tiger {
    public class MusicManager {
        // const int TASK_DELAY = 100;

        // const float crossFadeTime = 2.0f;
        float _fading;
        AudioSource _current;
        AudioSource _previous;
        DataSO.MusicData _musicData;
        // readonly Queue<AudioClip> playlist = new();

        // Dictionary<MusicBundleType, DataSO.MusicBundle> _musicBundles;

        // AudioMixerGroup _musicMixerGroup;
        MusicBundleType _curType;

        List<AudioClip> _curBundle => _musicData.bundles[_curType].audioClips;
        bool _playOnLoop => _musicData.bundles[_curType].shouldLoopFirstClip;
        float _crossFadeTime => _musicData.crossFadeTime;


        MusicSourcesPair _musicSourcesPair;
        bool _sourcesAreReversed = true;

        // [SerializeField] List<AudioClip> initialPlaylist;
        // [SerializeField] AudioMixerGroup musicMixerGroup;

        public MusicManager(DataSO.MusicData musicData,
            MusicSourcesPair musicSourcesPair) {
            _musicData = musicData;
            // _musicMixerGroup = musicMixerGroup;
            _musicSourcesPair = musicSourcesPair;
        }

        // void Start() {
        //     foreach (var clip in initialPlaylist) {
        //         AddToPlaylist(clip);
        //     }
        // }

        public void LoadBundle(MusicBundleType bundleType) {
            _curType = bundleType;

            // Clear();
            // var bundle = _musicBundles[bundleType];
            // bundle.audioClips.ForEach(AddToPlaylist);
            // _playOnLoop = bundle.shouldLoopFirstClip;
        }

        // public void AddToPlaylist(AudioClip clip) {
        //     playlist.Enqueue(clip);
        //     // if (current == null && previous == null) {
        //     //     PlayNextTrack();
        //     // }
        // }

        // public void Clear() => playlist.Clear();
        // public void Try() => PlayNextTrack();

        public void CheckForCrossFade() {
            // HandleCrossFade();

            if (!_current.clip) return;
            
            if ( _current.clip.length - _current.time <= _crossFadeTime) PlayNextTrack();

            // if (current && !current.isPlaying && playlist.Count > 0) PlayNextTrack();
        }

        public void PlayNextTrack() {
            CorrectSources();

            if (_playOnLoop) Play(_curBundle[0]);

            // if (playlist.TryDequeue(out var nextTrack)) Play(nextTrack);
        }

        public void Play(AudioClip clip) {
            // if (current && current.clip == clip) return;

            // if (previous) {
            //     Destroy(previous);
            //     previous = null;
            // }

            // previous = current;

            // current = gameObject.AddComponent<AudioSource>();
            _current.clip = clip;
            // current.outputAudioMixerGroup = musicMixerGroup; // Set mixer group
            _current.loop = false; // For playlist functionality, we want tracks to play once
            _current.volume = 0;
            _current.bypassListenerEffects = true;
            _current.Play();

            CoroutineCenter.Instance.StartCoroutine(HandleCrossFadeExo());
        }

        void CorrectSources() {
            _sourcesAreReversed = !_sourcesAreReversed;
            _previous = _sourcesAreReversed ? _musicSourcesPair.sourceTwo : _musicSourcesPair.sourceOne;
            _current = _sourcesAreReversed ? _musicSourcesPair.sourceOne : _musicSourcesPair.sourceTwo;
        }


        // void HandleCrossFade() {
        //     if (fading <= 0f) return;
        //
        //     fading += Time.deltaTime;
        //
        //     var fraction = Mathf.Clamp01(fading / crossFadeTime);
        //
        //     // Logarithmic fade
        //     var logFraction = fraction.ToLogarithmicFraction();
        //
        //     if (previous) previous.volume = 1.0f - logFraction;
        //     if (current) current.volume = logFraction;
        //
        //     if (fraction >= 1) {
        //         fading = 0.0f;
        //         if (previous) {
        //             Destroy(previous);
        //             previous = null;
        //         }
        //     }
        // }


        IEnumerator HandleCrossFadeExo() {
            _fading = 0.001f;

            while (_fading > 0f) {
                // _fading += .01f;

                _fading += Time.deltaTime;

                var fraction = Mathf.Clamp01(_fading / _crossFadeTime);

                // Logarithmic fade
                var logFraction = fraction.ToLogarithmicFraction();

                if (_previous) _previous.volume = 1.0f - logFraction;
                if (_current) _current.volume = logFraction;

                if (fraction >= 1) _fading = 0.0f;
                // if (previous) {
                //     Destroy(previous);
                //     previous = null;
                // }
                yield return null;
            }
        }

        public struct MusicSourcesPair {
            public AudioSource sourceOne;
            public AudioSource sourceTwo;
        }
    }
}