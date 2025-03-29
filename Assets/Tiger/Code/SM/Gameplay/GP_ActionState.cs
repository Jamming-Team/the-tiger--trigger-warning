using System.Threading.Tasks;

namespace Tiger.Gameplay {
    public class GP_ActionState : GP_SceneState {

        int iter = 0;
        
        protected override void OnEnter() {
            base.OnEnter();
            Test();
        }

        async void Test() {
            await Task.Delay(2000);

            if (iter == 0) {
                RequestTransition<GP_TransitionStateCorrect>();
                iter++;
            }
            else {
                RequestTransition<GP_TransitionStateWrong>();
            }
            
        }
        


    }
}