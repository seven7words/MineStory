namespace Wsc.Input
{
    public interface IInput
    {
        float GetAxis(string axisName);
        bool GetButtonDown(string buttonName);
        bool GetButton(string buttonName);
        bool GetButtonUp(string buttonName);
    }
}