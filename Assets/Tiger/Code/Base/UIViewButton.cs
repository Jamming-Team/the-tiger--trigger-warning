using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tiger {
    [RequireComponent(typeof(Button))]
    public class UIViewButton : MonoBehaviour {
        public UIButtonTypes buttonType;
        [HideInInspector] public Button buttonReference;

        void Awake() {
            buttonReference = GetComponent<Button>();
        }
    }
}