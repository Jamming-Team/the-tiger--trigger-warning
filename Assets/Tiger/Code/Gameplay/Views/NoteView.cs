using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tiger {
    public class NoteView : MonoBehaviour {

        [SerializeField] NoteItem _noteItemPrefab;
        [SerializeField] GameObject _noteItemsRoot;
        [SerializeField] GameObject _noteItemsRootWrong;
        
        EventBinding<NoteChanged> _noteChangedBinding;
        List<NoteItem> _noteItems = new List<NoteItem>();
        List<NoteItem> _noteItemsWrong = new List<NoteItem>();

        void Start() {
            
            _noteChangedBinding = new EventBinding<NoteChanged>(FillNote);
            EventBus<NoteChanged>.Register(_noteChangedBinding);
            
        }

        void FillNote(NoteChanged noteChangedEvent) {
            Debug.Log(noteChangedEvent.notes.Count);
            
            _noteItems.ForEach(x => Destroy(x.gameObject));
            _noteItems.Clear();
            
            _noteItemsWrong.ForEach(x => Destroy(x.gameObject));
            _noteItemsWrong.Clear();

            for (int i = 0; i < noteChangedEvent.notes.Count; i++) {
                NoteItem newNoteItem = null;
                if (noteChangedEvent.notes[i].type != NoteController.NoteItemType.Wrong) {
                    newNoteItem = Instantiate(_noteItemPrefab, _noteItemsRoot.transform);
                    _noteItems.Add(newNoteItem);
                }
                else {
                    newNoteItem = Instantiate(_noteItemPrefab, _noteItemsRootWrong.transform);
                    _noteItemsWrong.Add(newNoteItem);
                }
                newNoteItem?.Init(i + 1, noteChangedEvent.notes[i].data.sprite, noteChangedEvent.notes[i].type, noteChangedEvent.notes[i].data.GetColor());

            }
            
            
        }
    }
}