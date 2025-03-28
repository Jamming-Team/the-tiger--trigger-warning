using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tiger {
    [CreateAssetMenu(fileName = "Tiger", menuName = "Tiger", order = 0)]
    public class DataSO : ScriptableObject {
        public SystemData system;
        public GameData game;

        [Serializable]
        public class SystemData {
            public float musicVolume;
            public float sfxVolume;
        }

        [Serializable]
        public class GameData {
            public string test = "Tis WORKING!";
        }
    }
}