using System;
using TMPro;
using UnityEngine;

namespace Tiger {
    public class LivesView : MonoBehaviour {
        [SerializeField] TMP_Text _livesText;

        EventBinding<OnLivesCountChanged> _onLivesCountChangedBinding;

        void Awake() {
            
            _onLivesCountChangedBinding = new EventBinding<OnLivesCountChanged>(x => _livesText.text = x.count.ToString());
            EventBus<OnLivesCountChanged>.Register(_onLivesCountChangedBinding);
        }

        void OnDestroy() {
            EventBus<OnLivesCountChanged>.Deregister(_onLivesCountChangedBinding);
        }
    }
}