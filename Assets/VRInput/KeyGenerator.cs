﻿using UnityEngine;
using System;

[Serializable]
public class KeySet
{
    public bool IsTapTrigger;
    public string Tap;
    public string Snap;
    public string Left;
    public string Up;
    public string Right;
    public string Down;
}

[Serializable]
public class KeyMap
{
    public KeySet Center;
    public KeySet Left;
    public KeySet Up;
    public KeySet Right;
    public KeySet Down;
}

[Serializable]
public class KeyMaster
{
    public KeyMap Off;
    public KeyMap Center;
    public KeyMap Left;
    public KeyMap Up;
    public KeyMap Right;
    public KeyMap Down;
}

public class KeyGenerator : MonoBehaviour
{
    [SerializeField]
    PadState LeftPad;
    [SerializeField]
    PadState RightPad;
    [SerializeField]
    KeyMaster KeyMaster;
    [SerializeField]
    SnapEvent SnapEvent;
    [SerializeField]
    TapEvent TapEvent;

    public event Action<string> Key;

    public void Start()
    {
        TapEvent.OnTap += OnTap; // (Stateの変更後に叩かれないといけないという)順番依存あるけどまあ動いてるし……
        SnapEvent.LRoll += OnSnap;
        SnapEvent.Left += OnLeft;
        SnapEvent.Up += OnUp;
        SnapEvent.Right += OnRight;
        SnapEvent.Down += OnDown;
    }

    KeySet GetCurrentSet()
    {
        switch (LeftPad.GetState())
        {
            case PadState.State.Off: return GetCurrentSet(KeyMaster.Off);
            case PadState.State.Center: return GetCurrentSet(KeyMaster.Center);
            case PadState.State.Left: return GetCurrentSet(KeyMaster.Left);
            case PadState.State.Up: return GetCurrentSet(KeyMaster.Up);
            case PadState.State.Right: return GetCurrentSet(KeyMaster.Right);
            case PadState.State.Down: return GetCurrentSet(KeyMaster.Down);
            default: throw new Exception();
        }
    }

    KeySet GetCurrentSet(KeyMap map)
    {
        switch (RightPad.GetState())
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

    void OnTap()
    {
        var current = GetCurrentSet();
        if (current != null && current.IsTapTrigger) Invoke(current.Tap);
    }

    void OnNontap(Func<KeySet, string> mapper)
    {
        var current = GetCurrentSet();
        if (current != null && !current.IsTapTrigger) Invoke(mapper(current));
    }

    void OnSnap() => OnNontap(c => c.Snap);

    void OnLeft() => OnNontap(c => c.Left);

    void OnUp() => OnNontap(c => c.Up);

    void OnRight() => OnNontap(c => c.Right);

    void OnDown() => OnNontap(c => c.Down);

    void Invoke(string key)
    {
        if (!string.IsNullOrEmpty(key)) Key?.Invoke(key);
    }
}