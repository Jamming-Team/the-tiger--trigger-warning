using System;
using System.Reflection;
using UnityEngine;
using static Tiger.DataSO;

namespace Tiger {
    public class DataManager : IVisitor {
        DataSO _dataSO;
        
        EventBinding<UISliderChanged> _uiSliderBinding;

        public DataManager(DataSO dataSO) => _dataSO = dataSO;

        
        
        public void TrySupply(IVisitable requester) {
            requester.Accept(this);
        }
        
        public void Visit(object o)
        {
            MethodInfo visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { typeof(object) }))
            {
                visitMethod.Invoke(this, new object[] { o });
            }
            else
            {
                DefaultVisit(o);
            }
        }
        
        void DefaultVisit(object o)
        {
            // noop (== `no op` == `no operation`)
            Debug.Log("MCDataFillerVisitor.DefaultVisit");
        }
        
        public void Visit(GP_SceneController requester)
        {
            requester.data = _dataSO.game;
        }
        
        public void Visit(AudioManager requester)
        {
            requester.data = _dataSO.audio;
        }
    }
}