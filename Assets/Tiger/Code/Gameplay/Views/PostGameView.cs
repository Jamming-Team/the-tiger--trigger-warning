using TMPro;
using UnityEngine;

namespace Tiger {
    public class PostGameView : MonoBehaviour {

        [SerializeField] TMP_Text _postGameText;
        
        
        EventBinding<TisTheEnd> _tisTheEndBinding;

        void Start() {
            _tisTheEndBinding = new EventBinding<TisTheEnd>(SetPostGameBehaviour);
            EventBus<TisTheEnd>.Register(_tisTheEndBinding);
        }

        void OnDestroy() {
            EventBus<TisTheEnd>.Deregister(_tisTheEndBinding);
        }

        void SetPostGameBehaviour(TisTheEnd tisTheEnd) {
            _postGameText.text = tisTheEnd.isVictory ? "VICTORY!" : "GAME OVER!";
        }
        
        
    }
}