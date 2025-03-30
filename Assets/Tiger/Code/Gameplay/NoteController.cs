using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class NoteController : MonoBehaviour {
        List<NoteItem> _initialNoteItems = new List<NoteItem>();
        List<NoteItem> _finalNoteItems = new List<NoteItem>();

        public void FillInitialNotes(List<DataSO.ObjectData> dataList) {
            _initialNoteItems.Clear();
            
            dataList.ForEach(x => _initialNoteItems.Add(new NoteItem
            {
                data = x,
                type = NoteItemType.Normal
            }));
            
            EventBus<NoteChanged>.Raise(new NoteChanged
            {
                notes = _initialNoteItems
            });
        }

        public bool FillFinalNotes(List<DataSO.ObjectData> dataList) {
            _finalNoteItems.Clear();

            var didCorrect = true;
            
            _initialNoteItems.ForEach(x => {
                var item = x;
                var foundItem = dataList.Find(y => y.IsTheSame(x.data));
                if (foundItem != null) {
                    dataList.Remove(foundItem);
                    x.type = NoteItemType.Correct;
                    _finalNoteItems.Add(x);
                }
                else {
                    didCorrect = false;
                    _finalNoteItems.Add(x);
                }
            });

            dataList.ForEach(x => {
                _finalNoteItems.Add(new NoteItem {
                    data = x,
                    type = NoteItemType.Wrong
                });
                didCorrect = false;
            });
            
            EventBus<NoteChanged>.Raise(new NoteChanged
            {
                notes = _finalNoteItems
            });
            
            return didCorrect;
        }
        
        
        
        public class NoteItem {
            public DataSO.ObjectData data;
            public NoteItemType type;
        }
        
        public enum NoteItemType { Normal, Correct, Wrong }
    }
}