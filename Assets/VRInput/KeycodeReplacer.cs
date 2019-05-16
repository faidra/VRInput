using UnityEngine;
using WindowsInput.Native;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class ReplacerInfo
{
    [Serializable]
    public class ReplaceInfo
    {
        public VirtualKeyCode From;
        public VirtualKeyCode To;
    }
    public ReplaceInfo[] ReplaceInfos;
}

public class KeycodeReplacer : MonoBehaviour
{
    [SerializeField]
    ReplacerInfo ReplacerInfo;

    Dictionary<VirtualKeyCode, VirtualKeyCode> _map;

    void Start()
    {
        _map = ReplacerInfo.ReplaceInfos.ToDictionary(m => m.From, m => m.To);
    }

    public VirtualKeyCode Replace(VirtualKeyCode from)
    {
        if (_map.TryGetValue(from, out var to)) return to;
        return from;
    }
}
