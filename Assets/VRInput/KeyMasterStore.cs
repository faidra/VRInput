using UnityEngine;
using UniRx;

public class KeyMasterStore : MonoBehaviour
{
    [SerializeField]
    KeyMasterEditor Editor;

    public KeyMaster KeyMaster { get; private set; }

    void Start()
    {
        Editor.Set(KeyMasterIO.Read());
        Editor.OnValueChangedAsObservable()
            .Subscribe(OnChanged)
            .AddTo(this);
    }

    void OnChanged(KeyMaster value)
    {
        KeyMaster = value;
        KeyMasterIO.Write(value);
    }
}