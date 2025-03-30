using TMPro;
using UnityEngine;

namespace Tiger {
    public class PauseView : MonoBehaviour {

        [SerializeField] TMP_Text _tilIncreaseText;
        [SerializeField] TMP_Text _victoryCondText;
        
        EventBinding<GameFlowNumsChanged> _gameFlowNumsChangedBinding;

        void Start() {
            _gameFlowNumsChangedBinding = new EventBinding<GameFlowNumsChanged>(SetNewNums);
            EventBus<GameFlowNumsChanged>.Register(_gameFlowNumsChangedBinding);
        }

        void OnDestroy() {
            EventBus<GameFlowNumsChanged>.Deregister(_gameFlowNumsChangedBinding);
        }

        void SetNewNums(GameFlowNumsChanged tisTheEnd) {
            _tilIncreaseText.text = tisTheEnd.tilIncr.ToString();
            _victoryCondText.text = "> " + tisTheEnd.victCond + " OBJ";
        }
        
        
    }
}