using System;

namespace Wsc.Behaviour
{
    public interface IBehaviourEvent
    {
        event Action BeforUpdate;
        event Action OnUpdate;
        event Action AfterUpdate;
    }
}