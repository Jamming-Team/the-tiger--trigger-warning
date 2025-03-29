using UnityEngine;

namespace Tiger
{
    public class UsualItem : ClickableObject
    {
        public override void OnClick()
        {
            Debug.Log("Usual item Clicked");
        }
    }
}