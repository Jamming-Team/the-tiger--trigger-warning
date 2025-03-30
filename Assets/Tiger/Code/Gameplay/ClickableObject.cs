using System;
using UnityEngine;

namespace Tiger
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        
        [HideInInspector]
        public DataSO.ObjectData variantData;


        public void Init(DataSO.ObjectData data) {
            _spriteRenderer.sprite = data.sprite;
            _spriteRenderer.color = data.GetColor();
        }
        
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