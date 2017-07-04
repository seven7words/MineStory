using System;
using UnityEngine;
using Wsc.Behaviour;
using Wsc.STL;

namespace Wsc.Input
{
    public class HardwareInput
    {
        private HardwareInput()
        {
            keyboards = new DictionaryWithListKey<KeyCode, string>(null);
            axises = new DictionaryWithListKey<string, string>(null);
        }
        public HardwareInput(IBehaviourEvent behaviour, IInput input) : this()
        {
            this.input = input;
            this.behaviour = behaviour;
            behaviour.BeforUpdate += Update;
        }
        ~HardwareInput()
        {
            behaviour.BeforUpdate -= Update;
            behaviour = null;
            input = null;
            keyboards.Clear();
            keyboards = null;
            axises.Clear();
            axises = null;
        }
        private IBehaviourEvent behaviour;
        private IInput input;

        void Update()
        {
            keyboards.Traversal(TraversalKeyBoard);
            axises.Traversal(TraversalAxis);
        }

        private DictionaryWithListKey<KeyCode, string> keyboards;
        private bool TraversalKeyBoard(KeyCode code, string key)
        {
            if (UnityEngine.Input.GetKeyDown(code))
            {
                input.SetButtonDown(key);
            }
            if (UnityEngine.Input.GetKey(code))
            {
                input.SetButton(key);
            }
            if (UnityEngine.Input.GetKeyUp(code))
            {
                input.SetButtonUp(key);
            }
            return false;
        }

        private DictionaryWithListKey<string, string> axises;
        private bool TraversalAxis(string code, string key)
        {
            input.SetAxis(key, UnityEngine.Input.GetAxis(code));
            return false;
        }
    }
}