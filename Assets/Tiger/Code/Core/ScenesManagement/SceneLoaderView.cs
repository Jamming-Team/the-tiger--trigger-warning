using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tiger {
    public class SceneLoaderView : MonoBehaviour {
        
        [SerializeField] private GameObject _loadingObject;
        
        public void EnableLoadingCanvas(bool enable = true) {
            Debug.Log("EnableLoadingCanvas : " + enable);
            _loadingObject.SetActive(enable);
        }
    }
}