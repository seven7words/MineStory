using System;
using UnityEngine;
using Wsc.Behaviour;

namespace Wsc.Input
{
    public class InputManager : IInput, IOutput
    {
        private InputManager()
        {
            Axis = new Bus<float>(0);
            ButtonDown = new Bus<bool>(false);
            Button = new Bus<bool>(false);
            ButtonUp = new Bus<bool>(false);
        }
        public InputManager(IBehaviourEvent behaviour) : this()
        {
            this.behaviour = behaviour;
            behaviour.AfterUpdate += Reset;

            hardwareInput = new HardwareInput(behaviour, this);
        }
        ~InputManager()
        {
            behaviour.AfterUpdate -= Reset;
            behaviour = null;
            Axis = null;
            ButtonDown = null;
            Button = null;
            ButtonUp = null;
        }
        void Reset()
        {
            Axis.Reset();
            ButtonDown.Reset();
            Button.Reset();
            ButtonUp.Reset();
        }

        private IBehaviourEvent behaviour;
        #region Axis
        private Bus<float> Axis;
        #endregion

        #region Button
        private Bus<bool> ButtonDown;
        private Bus<bool> Button;
        private Bus<bool> ButtonUp;
        #endregion

        float IOutput.GetAxis(string axisName)
        {
            return Axis.Get(axisName);
        }

        bool IOutput.GetButtonDown(string buttonName)
        {
            return ButtonDown.Get(buttonName);
        }

        bool IOutput.GetButton(string buttonName)
        {
            return Button.Get(buttonName);
        }

        bool IOutput.GetButtonUp(string buttonName)
        {
            return ButtonUp.Get(buttonName);
        }

        void IInput.SetAxis(string axisName, float value)
        {
            Axis.Set(axisName, value);
        }

        void IInput.SetButtonDown(string buttonName)
        {
            ButtonDown.Set(buttonName, true);
        }

        void IInput.SetButton(string buttonName)
        {
            Button.Set(buttonName, true);
        }

        void IInput.SetButtonUp(string buttonName)
        {
            ButtonUp.Set(buttonName, true);
        }


        #region HardwareInput
        private HardwareInput hardwareInput;
        public void AddKeyBoardInput(KeyCode code, string key)
        {
            hardwareInput.AddKeyBoard(code, key);
        }
        public void AddAxisInput(string code, string key)
        {
            hardwareInput.AddAxis(code, key);
        }
        #endregion


    }

}
