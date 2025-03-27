using System;
using UnityEngine;

namespace Tiger.MainMenu {
    public class Testing : MonoBehaviour {
        private void Start() {
            Debug.Log(GameManager.HasInstance);
        }
    }
}