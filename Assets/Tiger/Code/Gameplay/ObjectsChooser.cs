using System.Collections.Generic;
using UnityEngine;

namespace Tiger {
    public class ObjectsChooser {
        
        public List<DataSO.ObjectData> _variantsThatShouldPresent = new();
        public List<DataSO.ObjectData> _objectVariants;

        List<DataSO.ObjectData> _remainingVariantsList = new();

        public ObjectsChooser(List<DataSO.ObjectData> objectVariants) {
            _objectVariants = objectVariants;
        }
        
        public void ClearVariantsList() {
            _variantsThatShouldPresent.Clear();
            _remainingVariantsList.Clear();
            _objectVariants.ForEach(x => _remainingVariantsList.Add(x));
        }

        public List<DataSO.ObjectData> GetVariantsList(int count) {
            var returnList = new List<DataSO.ObjectData>();
            returnList.AddRange(_variantsThatShouldPresent);
            var iter = 0;
            var attempts = 0;
            
            
            
            
            while (iter < count) {
                var randomInt = Random.Range(0, _remainingVariantsList.Count);

                    returnList.Add(_remainingVariantsList[randomInt]);
                    _variantsThatShouldPresent.Add(_remainingVariantsList[randomInt]);
                    iter++;
                    
                    _remainingVariantsList.RemoveAt(randomInt);
            }
            return returnList;
        }
        
    }
}