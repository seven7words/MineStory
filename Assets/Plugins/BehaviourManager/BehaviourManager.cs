using System;

namespace Wsc.Behaviour
{
    public interface IBehaviour
    {
        event Action BeforUpdate;
        event Action OnUpdate;
        event Action AfterUpdate;
    }
    public class BehaviourManager
    {

    }
}