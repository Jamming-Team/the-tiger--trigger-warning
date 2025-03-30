using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class GP_SceneController : SceneController, IVisitable {
        [SerializeField] ObjectSpawner _objectSpawner;
        [SerializeField] NoteController _noteController;
        [SerializeField] int _objectsCount;

        [HideInInspector] public NoteStates noteState = NoteStates.ViewUntilResume; 
        
            
            
        [HideInInspector]
        public DataSO.GameData data { get; set; }
        public bool freeActIsInAction => _stateMachine.currentState is Gameplay.GP_ActionState;
        
        ObjectsChooser _objectsChooser;
        List<ClickableObject> _clickableObjects = new List<ClickableObject>();
        
        EventBinding<OnItemClicked> _onItemClickedBinding;
        
        protected override void Start() {

            GameManager.Instance.RequestData(this);
            Debug.Log(data.test);
            
            _stateMachine.Init(this);

            _objectsChooser = new ObjectsChooser(data.objectVariantsData);
            
            _onItemClickedBinding = new EventBinding<OnItemClicked>(x => AdjustSelectedCollection(x.item, x.shouldAdd));
            EventBus<OnItemClicked>.Register(_onItemClickedBinding);

        }

        void OnDestroy() {
            EventBus<OnItemClicked>.Deregister(_onItemClickedBinding);
        }

        public void FillInitial() {
            _objectsChooser.ClearVariantsList();
            _noteController.FillInitialNotes(_objectsChooser.GetVariantsList(_objectsCount));
        }

        public void FillFinal() { 
            List<DataSO.ObjectData> finalObjectsData = new List<DataSO.ObjectData>();
            _clickableObjects.ForEach(x => finalObjectsData.Add(x.variantData));
            _noteController.FillFinalNotes(finalObjectsData);
        }

        public void SpawnThose() {
            _clickableObjects.Clear();
            _objectSpawner.DestroyAllObjects();
            _objectSpawner.SpawnObjects(_objectsChooser.GetVariantsList(_objectsCount));
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
        
        public enum NoteStates {
            ViewUntilUpdate,
            ViewUntilResume,
            ViewUntilExit,
        }
    }
}