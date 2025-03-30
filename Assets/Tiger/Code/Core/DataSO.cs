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
        public UIData ui;

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

            [Range(1, 9)]
            public int initialObjectsCount = 3;
            [Range(1, 9)]
            public int maxObjectsCount = 9;
            [Range(1, 3)]
            public int turnsTillIncrease = 1;
            [Range(0, 6)]
            public int livesCount = 3;
            
            [Range(1, 5)]
            public int objectsToAddOverNeeded = 5;

            [SerializedDictionary("Type", "Timings")]
            public SerializedDictionary<TransitionType, List<int>> transitions;
            
            public List<ObjectData> objectVariantsData;
        }
        
        
        
        [Serializable]
        public class ObjectData {
            public string name;
            public Sprite sprite;
            public ColorsEnum colorType = ColorsEnum.Normal;
            // [SerializedDictionary] public SerializedDictionary<ColorsEnum, Color> allowedColors;

            public bool IsTheSame(ObjectData other) {
                return name == other.name && sprite == other.sprite && colorType == other.colorType;
            }

            public Color GetColor() {
                switch (colorType) {
                    case ColorsEnum.Normal:
                        return Color.white;
                    case ColorsEnum.Red:
                        return Color.red;
                    case ColorsEnum.Blue:
                        return Color.blue;
                    case ColorsEnum.Yellow:
                        return Color.yellow;
                }
                return Color.white;
            }
        }
        
        [Serializable]
        public class ColorData {
            public ColorsEnum type;
            public Color color;
        }
        
        

        [Serializable]
        public class UIData {
            [SerializedDictionary("Type", "Trs msg")]
            public SerializedDictionary<UITransitionMessageTypes, TransitionMessage> transitionMsgs;
        }
        
        [Serializable]
        public class CameraData {
            public float zoomSensitivity = 10f;
            public Vector2 zoomConstraints = new Vector2(15f, 40f);
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

        [Serializable]
        public class TransitionMessage {
            public string message;
            public Color textColor;
        }
    }
    
    public enum MusicBundleType { MainMenu, Gameplay }
    public enum TransitionType {Intro, Correct, Wrong, Repeat}
    
    public enum ColorsEnum {Normal, Red, Blue, Yellow}

}