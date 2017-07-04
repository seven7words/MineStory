using Wsc.Behaviour;

namespace Wsc.Input
{
    public class InputManager : IInput
    {
        private InputManager()
        {
            Axis = new Bus<float>(0);
            ButtonDown = new Bus<bool>(false);
            Button = new Bus<bool>(false);
            ButtonUp = new Bus<bool>(false);
        }
        public InputManager(IBehaviour behaviour) : this()
        {
            behaviour.AfterUpdate += Reset;
        }
        void Reset()
        {
            Axis.Reset();
            ButtonDown.Reset();
            Button.Reset();
            ButtonUp.Reset();
        }

        #region Axis
        private Bus<float> Axis;
        #endregion

        #region Button
        private Bus<bool> ButtonDown;
        private Bus<bool> Button;
        private Bus<bool> ButtonUp;
        #endregion



        float IInput.GetAxis(string axisName)
        {
            return Axis.Get(axisName);
        }

        bool IInput.GetButtonDown(string buttonName)
        {
            return ButtonDown.Get(buttonName);
        }

        bool IInput.GetButton(string buttonName)
        {
            return Button.Get(buttonName);
        }

        bool IInput.GetButtonUp(string buttonName)
        {
            return ButtonUp.Get(buttonName);
        }
    }

}
