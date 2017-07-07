using System.Collections;
using UnityEngine;

namespace Wsc.Behaviour
{
    public interface IBehaviourHandler
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}