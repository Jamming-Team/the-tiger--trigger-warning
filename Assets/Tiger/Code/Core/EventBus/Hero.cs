using UnityEngine;

namespace Tiger {
    public class Hero : MonoBehaviour {
        [SerializeField] InputReader _inputReader;
        
        int health;
        int mana;

        EventBinding<TestEvent> testEventBinding;
        EventBinding<PlayerEvent> playerEventBinding;

        void OnEnable() {
            testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
            EventBus<TestEvent>.Register(testEventBinding);
            
            playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
            EventBus<PlayerEvent>.Register(playerEventBinding);
            _inputReader.EnablePlayerActions();
        }

        void OnDisable() {
            EventBus<TestEvent>.Deregister(testEventBinding);
            EventBus<PlayerEvent>.Deregister(playerEventBinding);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                EventBus<TestEvent>.Raise(new TestEvent());
            }

            if (Input.GetKeyDown(KeyCode.B)) {
                EventBus<PlayerEvent>.Raise(new PlayerEvent {
                    health = this.health,
                    mana = this.mana,
                }); 
            }

            if (_inputReader.interactIsBeingPressed) {
                Debug.Log("Interact");
            }

            if (_inputReader.rotateIsBeingPressed) {
                Debug.Log("Rotate");
            }
        }

        void HandleTestEvent() {
            Debug.Log("Test event received!");
        }

        void HandlePlayerEvent(PlayerEvent playerEvent) {
            Debug.Log($"Player event received: {playerEvent}");
        }
    }
}