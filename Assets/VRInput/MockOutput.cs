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
        Text.text = key;
    }
}
