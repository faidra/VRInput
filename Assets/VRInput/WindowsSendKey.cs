using UnityEngine;
using WindowsInput;
using WindowsInput.Native;
using System.Linq;
using System.Collections.Generic;

public class WindowsSendKey : MonoBehaviour
{
    [SerializeField]
    KeyGenerator KeyGenerator;
    [SerializeField]
    AdvancedSettingsStore AdvancedSettingsStore;

    IEnumerable<KeyMapping> KeyMappings => AdvancedSettingsStore.AdvancedSettings.KeyMappings;
    KeyReplacing KeyReplacing => AdvancedSettingsStore.AdvancedSettings.KeyReplacing;

    IKeyboardSimulator KeyboardSimulator;

    void Start()
    {
        KeyboardSimulator = new InputSimulator().Keyboard;
        KeyGenerator.Key += OnKey;
    }

    void OnKey(string str)
    {
        foreach (var mapper in KeyMappings)
        {
            var codes = mapper.Map(str);
            if (codes != null)
            {
                Send(codes.Select(c => KeyReplacing.Replace(c)));
                return;
            }
        }
    }

    void Send(IEnumerable<VirtualKeyCode> codes)
    {
        // todo: 同時押し対応
        foreach (var code in codes) KeyboardSimulator.KeyPress(code);
    }
}
