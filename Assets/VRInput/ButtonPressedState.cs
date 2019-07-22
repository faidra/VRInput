using UnityEngine;

public class ButtonPressedState : MonoBehaviour
{
    public enum State
    {
        Off,
        On,
    }

    [SerializeField]
    RawPad RawPad;
    [SerializeField]
    ulong Button;

    public State GetState()
    {
        if ((RawPad.ButtonPressed & Button) != 0) return State.On;
        else return State.Off;
    }
}
