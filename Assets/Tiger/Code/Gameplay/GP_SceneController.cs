using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class GP_SceneController : SceneController, IVisitable {
        [SerializeField] ObjectSpawner _objectSpawner;
        [SerializeField] NoteController _noteController;
        [SerializeField] int _objectsCount;
        
        [HideInInspector]
        public DataSO.GameData data { get; set; }
        public bool freeActIsInAction => _stateMachine.currentState is Gameplay.GP_ActionState;
        
        ObjectsChooser _objectsChooser;
        List<ClickableObject> _clickableObjects;

        
        protected override void Start() {

            GameManager.Instance.RequestData(this);
            Debug.Log(data.test);
            
            _stateMachine.Init(this);

            _objectsChooser = new ObjectsChooser(data.objectVariantsData);

        }

        public void FillInitial() {
            _objectsChooser.ClearVariantsList();
            _noteController.FillInitialNotes(_objectsChooser.GetVariantsList(_objectsCount));
        }

        void AdjustSelectedCollection(ClickableObject clickableObject, bool shouldAdd) {
            if (shouldAdd)
                _clickableObjects.Add(clickableObject);
            else {
                _clickableObjects.Remove(clickableObject);
            }
        }
        

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }


    }
}