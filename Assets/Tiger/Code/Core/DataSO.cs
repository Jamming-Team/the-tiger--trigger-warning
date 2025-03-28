using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tiger {
    [CreateAssetMenu(fileName = "Data", menuName = "Tiger/Data", order = 0)]
    public class DataSO : ScriptableObject {
        public AudioData audio;
        public GameData game;

        [Serializable]
        public class AudioData {
            public float musicVolume;
            public float sfxVolume;
            public MusicData music;
        }

        [Serializable]
        public class GameData {
            public string test = "Tis WORKING!";
            public CameraData camera;
        }

        [Serializable]
        public class CameraData {
            public Vector2 verticalAxisConstraint = new Vector2(10f, 50f);
            public Vector2 rotationSensitivity = new Vector2(400f, 200f);
        }
        
        [Serializable]
        public class MusicBundle {
            public List<AudioClip> audioClips;
            public bool shouldLoopFirstClip = false;
        }

        [Serializable]
        public class MusicData {
            // public List<AudioClip> audioClips;
            [SerializedDictionary("Type", "Bundle")]
            public SerializedDictionary<MusicBundleType, MusicBundle> bundles;
            public float crossFadeTime = 2.0f;
        }
    }
    
    public enum MusicBundleType { MainMenu, Gameplay }
}