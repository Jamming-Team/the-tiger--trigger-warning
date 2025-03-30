using System.Collections;
using UnityEngine;

namespace Tiger.Gameplay {
    public class GP_TransitionStateCorrect : GP_TransitionState {
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            _transitionTimings = _core.data.transitions[TransitionType.Intro];
        }

        protected override void OnEnter() {
            base.OnEnter();
            StartCoroutine(PerformTransition());
        }

        IEnumerator PerformTransition() {
               


            // Hide Walls here

            _core.SpawnThose();
            
            
            yield return new WaitForSeconds(0.1f);


            

            RequestTransition<GP_ActionState>();
        }


    }
}