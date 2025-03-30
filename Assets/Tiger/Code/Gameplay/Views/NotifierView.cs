using TMPro;
using UnityEngine;

namespace Tiger {
    public class NotifierView : MonoBehaviour, IVisitable {
        [SerializeField] TMP_Text _messageText;
        [SerializeField] GameObject _notifierPanelGO;

        public DataSO.UIData data { get; set; }

        EventBinding<UISetTransitionMsg> _setTransitionMsgBinding;

        void Start() {
            GameManager.Instance.RequestData(this);
            
            _setTransitionMsgBinding = new EventBinding<UISetTransitionMsg>(SetMsg);
            EventBus<UISetTransitionMsg>.Register(_setTransitionMsgBinding);
        }
        
        void OnDestroy() {
            EventBus<UISetTransitionMsg>.Deregister(_setTransitionMsgBinding);
        }

        void SetMsg(UISetTransitionMsg transitionMsgEvent) {
            if (transitionMsgEvent.type == UITransitionMessageTypes.None) {
                _notifierPanelGO.SetActive(false);
                return;
            }

            _notifierPanelGO.SetActive(true);
            _messageText.text = data.transitionMsgs[transitionMsgEvent.type].message;
            _messageText.color = data.transitionMsgs[transitionMsgEvent.type].textColor;
        }

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }
    }
}