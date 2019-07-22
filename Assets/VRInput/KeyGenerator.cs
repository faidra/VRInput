using UnityEngine;
using System;

public class KeyGenerator : MonoBehaviour
{
    [SerializeField]
    PadState PadState;
    [SerializeField]
    KeyMasterStore KeyMasterStore;
    [SerializeField]
    ButtonPressedState TriggerState;
    [SerializeField]
    ButtonPressedState PadPressState;

    KeyMaster KeyMaster => KeyMasterStore.KeyMaster;
    public event Action<string> Key;

    bool _lastPadPressed;

    void Update()
    {
        var pressed = PadPressState.GetState() == ButtonPressedState.State.On;
        if (pressed && !_lastPadPressed) Invoke();
        _lastPadPressed = pressed;
    }

    KeySet GetCurrentSet()
    {
        switch (TriggerState.GetState())
        {
            case ButtonPressedState.State.Off: return GetCurrentSet(KeyMaster.Off);
            case ButtonPressedState.State.On: return GetCurrentSet(KeyMaster.Center);
            default: throw new Exception();
        }
    }

    KeySet GetCurrentSet(KeyMap map)
    {
        switch (PadState.GetState())
        {
            case PadState.State.Off: return null;
            case PadState.State.Center: return map.Center;
            case PadState.State.Left: return map.Left;
            case PadState.State.Up: return map.Up;
            case PadState.State.Right: return map.Right;
            case PadState.State.Down: return map.Down;
            default: throw new Exception();
        }
    }

    void Invoke()
    {
        var current = GetCurrentSet();
        Debug.Log(current?.Snap);
        if (current != null) Invoke(current.Snap);
    }

    void Invoke(string key)
    {
        if (!string.IsNullOrEmpty(key)) Key?.Invoke(key);
    }
}