namespace Wsc.Input
{
    public interface IInput
    {
        void SetAxis(string axisName, float value);
        void SetButtonDown(string buttonName);
        void SetButton(string buttonName);
        void SetButtonUp(string buttonName);
    }
}