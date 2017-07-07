using System.Collections;
using UnityEngine;

namespace Wsc.Behaviour
{
    public interface IBehaviourHandle
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}