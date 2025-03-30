using System.Threading.Tasks;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateRepeat : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Repeat];
        }

        protected override void OnEnter() {
            base.OnEnter();
            PerformTransition();
        }

        async void PerformTransition() {


            // Show walls
            _core.noteState = GP_SceneController.NoteStates.ViewUntilUpdate;

            var tisDefeat = _core.FillFinal();


            if (tisDefeat) {
                EventBus<TisTheEnd>.Raise(new TisTheEnd {
                    isVictory = false
                });
                _core.noteState = GP_SceneController.NoteStates.ViewUntilExit;
            }
            // else {
            //     _core.noteState = GP_SceneController.NoteStates.ViewUntilUpdate;
            // }
            
            


            await Task.Delay(100);
            


            RequestTransition<GP_NoteState>();

        }
    }
}