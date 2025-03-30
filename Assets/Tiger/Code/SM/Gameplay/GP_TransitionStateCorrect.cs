using System.Threading.Tasks;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateCorrect : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Intro];
        }

        protected override void OnEnter() {
            base.OnEnter();
            PerformTransition();
        }

        async void PerformTransition() {


            


            


            // Hide Walls here

            _core.SpawnThose();
            
            
            await Task.Delay(100);


            

            RequestTransition<GP_ActionState>();

        }
    }
}