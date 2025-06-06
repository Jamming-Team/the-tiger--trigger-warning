using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class GP_SceneController : SceneController, IVisitable {
        [SerializeField] ObjectSpawner _objectSpawner;
        [SerializeField] NoteController _noteController;
        [SerializeField] int _objectsCount;

        [SerializeField] GameObject _walls;
        
        [HideInInspector] public NoteStates noteState = NoteStates.ViewUntilResume; 
        
            
        [HideInInspector]
        public DataSO.GameData data { get; set; }
        public bool freeActIsInAction => _stateMachine.currentState is Gameplay.GP_ActionState;
        
        ObjectsChooser _objectsChooser;
        List<ClickableObject> _clickableObjects = new List<ClickableObject>();
        
        EventBinding<OnItemClicked> _onItemClickedBinding;
        
        int _livesCount;
        int _curObjCount;
        int _turnsTillObjIncrease;
        
        protected override void Start() {

            GameManager.Instance.RequestData(this);
            Debug.Log(data.test);

            _livesCount = data.livesCount == 0 ? 9999 : data.livesCount;
            _curObjCount = data.initialObjectsCount;
            _turnsTillObjIncrease = data.turnsTillIncrease;
            
            _stateMachine.Init(this);

            _objectsChooser = new ObjectsChooser(data.objectVariantsData);
            
            _onItemClickedBinding = new EventBinding<OnItemClicked>(x => AdjustSelectedCollection(x.item, x.shouldAdd));
            EventBus<OnItemClicked>.Register(_onItemClickedBinding);
            
            EventBus<OnLivesCountChanged>.Raise(new OnLivesCountChanged {
                count = _livesCount
            });
            
            EventBus<GameFlowNumsChanged>.Raise(new GameFlowNumsChanged {
                tilIncr = _turnsTillObjIncrease,
                victCond = data.maxObjectsCount
            });

        }

        void OnDestroy() {
            EventBus<OnItemClicked>.Deregister(_onItemClickedBinding);
        }

        public bool DefeatOrNo() {
            _livesCount--;
            EventBus<OnLivesCountChanged>.Raise(new OnLivesCountChanged {
                count = _livesCount
            });
            
            if (_livesCount == 0)
                return true;
            return false;
        }

        public void FillInitial() {
            _objectsChooser.ClearVariantsList();
            _noteController.FillInitialNotes(_objectsChooser.GetVariantsList(_curObjCount));
        }

        public bool FillFinal() {
            var shouldLose = false;
            List<DataSO.ObjectData> finalObjectsData = new List<DataSO.ObjectData>();
            _clickableObjects.ForEach(x => finalObjectsData.Add(x.variantData));
            if (!_noteController.FillFinalNotes(finalObjectsData)) {
                shouldLose = DefeatOrNo();
                EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
                {
                    type = UITransitionMessageTypes.Wrong
                });
            }
            else {
                EventBus<UISetTransitionMsg>.Raise(new UISetTransitionMsg
                {
                    type = UITransitionMessageTypes.Correct
                });
                _turnsTillObjIncrease--;
                if (_turnsTillObjIncrease == 0) {
                    _curObjCount++;
                    if (data.maxObjectsCount < _curObjCount) {
                        EventBus<TisTheEnd>.Raise(new TisTheEnd {
                            isVictory = true
                        });
                        noteState = NoteStates.ViewUntilExit;
                    }
                    else {
                        _turnsTillObjIncrease = data.turnsTillIncrease;
                    }
                }
                EventBus<GameFlowNumsChanged>.Raise(new GameFlowNumsChanged {
                    tilIncr = _turnsTillObjIncrease,
                    victCond = data.maxObjectsCount
                });
            }
            
            
            
            return shouldLose;
        }

        public void ChangeWallsVisibility(bool visibility) {
            _walls.SetActive(visibility);
        }

        public void SpawnThose() {
            _clickableObjects.Clear();
            _objectSpawner.DestroyAllObjects();
            _objectSpawner.SpawnObjects(_objectsChooser.GetVariantsList(data.objectsToAddOverNeeded));
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