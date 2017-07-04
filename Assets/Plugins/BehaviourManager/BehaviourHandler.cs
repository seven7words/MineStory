using System;
using UnityEngine;

namespace Wsc.Behaviour
{
    public class BehaviourHandler : MonoBehaviour
    {
        public event Action BeforUpdate;
        public event Action OnUpdate;
        public event Action AfterUpdate;

        void Update()
        {
            if (BeforUpdate != null) { BeforUpdate(); }
            if (OnUpdate != null) { OnUpdate(); }
        }

        void LateUpdate()
        {
            if (AfterUpdate != null) { AfterUpdate(); }
        }

        internal void Clear()
        {
            BeforUpdate = null;
            OnUpdate = null;
            AfterUpdate = null;
        }
    }
}