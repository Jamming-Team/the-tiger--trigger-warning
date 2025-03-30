using System;
using UnityEngine;

namespace Tiger
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;

        [SerializeField] GameObject _onHoverObject;
        [SerializeField] GameObject _onSelectedObject;
        
        [SerializeField] SoundData _onHoverSoundData;
        [SerializeField] SoundData _onSelectSoundData;
        [SerializeField] SoundData _onDeselectSoundData;

        
        [HideInInspector]
        public DataSO.ObjectData variantData;
        
        
        
        bool _isSelected = false;


        public void Init(DataSO.ObjectData data) {
            variantData = data;
            _spriteRenderer.sprite = data.sprite;
            _spriteRenderer.color = data.GetColor();
        }

        void OnMouseEnter() {
            if (!Cursor.visible)
                return;
            _onHoverObject.SetActive(true);
            AudioManager.Instance.PlaySound(_onHoverSoundData, transform);
        }

        public void OnMouseOver() {

        }

        public void OnMouseExit() {
            _onHoverObject.SetActive(false);
        }

        public void ClickTrigger()
        {
            OnClick();
        }
        
        public virtual void OnClick()
        {
            Debug.Log("Clickable Object Clicked");
            _isSelected = !_isSelected;
            _onSelectedObject.SetActive(_isSelected);
            EventBus<OnItemClicked>.Raise(new OnItemClicked
            {
                item = this,
                shouldAdd = _isSelected
            });
            
            AudioManager.Instance.PlaySound(_isSelected ? _onSelectSoundData : _onDeselectSoundData, transform);
        }
    }
}