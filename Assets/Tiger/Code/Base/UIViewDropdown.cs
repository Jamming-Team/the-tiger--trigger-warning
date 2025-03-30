using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tiger {
    [RequireComponent(typeof(TMP_Dropdown))]
    public class UIViewDropdown : MonoBehaviour, IVisitable {
        public UIDropdownTypes dropdownType;
        [HideInInspector] public TMP_Dropdown dropdownReference;

        [HideInInspector] public DataSO.GameData data;

        void Awake() {
            dropdownReference = GetComponent<TMP_Dropdown>();
            
        }
        
        void Start() {

            GameManager.Instance.RequestData(this);

            switch (dropdownType) {
                case UIDropdownTypes.InitialObj: {
                    dropdownReference.value = data.initialObjectsCount - 1;
                    break;
                }
                case UIDropdownTypes.MaxObj: {
                    dropdownReference.value = data.maxObjectsCount - 5;
                    break;
                }
                case UIDropdownTypes.LivesCount: {
                    dropdownReference.value = data.livesCount;
                    break;
                }
                case UIDropdownTypes.TilIncrease: {
                    dropdownReference.value = data.turnsTillIncrease - 1;
                    break;
                }
            }
            
        }
        
        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}