using System;
using UnityEngine;

namespace Tiger
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        
        [HideInInspector]
        public DataSO.ObjectData variantData;
        
        bool _isSelected = false;


        public void Init(DataSO.ObjectData data) {
            variantData = data;
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
            _isSelected = !_isSelected;
            EventBus<OnItemClicked>.Raise(new OnItemClicked
            {
                item = this,
                shouldAdd = _isSelected
            });
        }
    }
}