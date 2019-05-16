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
    KeyMapper[] KeyMappers;
    [SerializeField]
    KeycodeReplacer KeycodeReplacer;

    IKeyboardSimulator KeyboardSimulator;

    void Start()
    {
        KeyboardSimulator = new InputSimulator().Keyboard;
        KeyGenerator.Key += OnKey;
    }

    void OnKey(string str)
    {
        foreach (var mapper in KeyMappers)
        {
            var codes = mapper.Map(str);
            if (codes != null)
            {
                Send(codes.Select(c => KeycodeReplacer.Replace(c)));
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
