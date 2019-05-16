using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MockOutput : MonoBehaviour
{
    [SerializeField]
    Text Text;
    [SerializeField]
    KeyGenerator KeyGenerator;

    void Start()
    {
        KeyGenerator.Key += OnKey;
    }

    void OnKey(string key)
    {
        switch (key)
        {
            case "{BS}": Delete(); break;
            case "{Enter}": Append("\n"); break;
            case "{Space}": Append(" "); break;
            case "{Kanji}": break;
            default: Append(key); break;
        }
    }

    void Append(string key)
    {
        Text.text += key;
    }

    void Delete()
    {
        Text.text = string.Concat(Text.text.Take(Text.text.Length - 1));
    }
}
