using System;
using UnityEngine;

public class TapEvent : MonoBehaviour
{
    [SerializeField]
    RawPad RawPad;

    public event Action OnTap;

    bool _lastTapped;

    void Update()
    {
        var isTapped = RawPad.InputVector.sqrMagnitude > 0f;
        if (isTapped && !_lastTapped) OnTap?.Invoke();
        _lastTapped = isTapped;
    }
}
