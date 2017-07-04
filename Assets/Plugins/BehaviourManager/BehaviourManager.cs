using System;
using UnityEngine;

namespace Wsc.Behaviour
{
    public class BehaviourManager : IBehaviourEvent
    {
        public BehaviourManager()
        {
            handler = new GameObject("BehaviourHandler", typeof(BehaviourHandler)).GetComponent<BehaviourHandler>();
            GameObject.DontDestroyOnLoad(handler.gameObject);
        }
        ~BehaviourManager()
        {
            handler.Clear();
            GameObject.Destroy(handler.gameObject);
        }
        BehaviourHandler handler;
        event Action IBehaviourEvent.BeforUpdate
        {
            add { handler.BeforUpdate += value; }
            remove { handler.BeforUpdate -= value; }
        }

        event Action IBehaviourEvent.OnUpdate
        {
            add { handler.OnUpdate += value; }
            remove { handler.OnUpdate -= value; }
        }

        event Action IBehaviourEvent.AfterUpdate
        {
            add { handler.AfterUpdate += value; }
            remove { handler.AfterUpdate -= value; }
        }
    }
}