using UnityEngine;
using WindowsInput.Native;
using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class KeyToCodesInfo
{
    [Serializable]
    public struct KeyToCode
    {
        public string Key;
        public VirtualKeyCode[] Codes;
    }
    public KeyToCode[] RawMap;
}

public class KeyMapper : MonoBehaviour
{
    [SerializeField]
    KeyToCodesInfo Info;

    Dictionary<string, VirtualKeyCode[]> _map;

    void Start()
    {
        _map = Info.RawMap.ToDictionary(m => m.Key, m => m.Codes);
    }

    public IEnumerable<VirtualKeyCode> Map(string str)
    {
        if (_map.TryGetValue(str, out var v)) return v;
        return null;
    }
}
