using System.Collections.Generic;
using WindowsInput.Native;

public class AdvancedSettings
{
    public KeyMapping[] KeyMappings;
    public KeyReplacing KeyReplacing;
}

public class AdvancedSettingsInfo
{
    public string[] KeyMappingPaths { get; set; }
    public string KeyReplacingPath { get; set; }
}

public class KeyMapping
{
    public Dictionary<string, VirtualKeyCode[]> Values { get; set; }

    public IEnumerable<VirtualKeyCode> Map(string str)
    {
        if (Values.TryGetValue(str, out var v)) return v;
        return null;
    }
}

public class KeyReplacing
{
    public static KeyReplacing Empty = new KeyReplacing { Values = new Dictionary<VirtualKeyCode, VirtualKeyCode>() };

    public Dictionary<VirtualKeyCode, VirtualKeyCode> Values { get; set; }

    public VirtualKeyCode Replace(VirtualKeyCode from)
    {
        if (Values.TryGetValue(from, out var to)) return to;
        return from;
    }
}