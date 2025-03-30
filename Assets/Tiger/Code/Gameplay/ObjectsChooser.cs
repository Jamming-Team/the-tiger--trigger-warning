using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class ObjectsChooser {
        
        public List<DataSO.ObjectData> _variantsThatShouldPresent = new();
        public List<DataSO.ObjectData> _objectVariants;

        public ObjectsChooser(List<DataSO.ObjectData> objectVariants) {
            _objectVariants = objectVariants;
        }
        
        public void ClearVariantsList() {
            _variantsThatShouldPresent.Clear();
        }

        public List<DataSO.ObjectData> GetVariantsList(int count) {
            var returnList = new List<DataSO.ObjectData>();
            returnList.AddRange(_variantsThatShouldPresent);
            var iter = 0;
            while (iter < count) {
                var randomInt = Random.Range(0, _objectVariants.Count);
                if (_variantsThatShouldPresent.Find(x => x.IsTheSame(_objectVariants[randomInt])) == null)
                {
                    returnList.Add(_objectVariants[randomInt]);
                    _variantsThatShouldPresent.Add(_objectVariants[randomInt]);
                    iter++;
                }
            }
            return returnList;
        }
        
    }
}