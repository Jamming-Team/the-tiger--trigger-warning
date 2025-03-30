using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tiger {
    public class NoteItem : MonoBehaviour {
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _image;
        [SerializeField] GameObject _strikethrough;

        public void Init(int itemIndex, Sprite icon, NoteController.NoteItemType type, Color color) {
            _text.text = itemIndex.ToString() + ". ";
            _image.sprite = icon;
            _image.color = color;
            switch (type) {
                case NoteController.NoteItemType.Correct:
                    _text.color = Color.green;
                    _strikethrough.SetActive(true);
                    break;
                case NoteController.NoteItemType.Wrong:
                    _text.color = Color.red;
                    break;
            }
        }
    }
}