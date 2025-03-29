using UnityEngine;

namespace Tiger
{
    public class ClickableObject : MonoBehaviour
    {
        public void ClickTrigger()
        {
            OnClick();
        }
        
        public virtual void OnClick()
        {
            Debug.Log("Clickable Object Clicked");
        }
    }
}