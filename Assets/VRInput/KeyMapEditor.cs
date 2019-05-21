using UnityEngine;
using System;
using UniRx;

public class KeyMapEditor : MonoBehaviour
{
    [SerializeField]
    KeySetEditor Center;
    [SerializeField]
    KeySetEditor Left;
    [SerializeField]
    KeySetEditor Up;
    [SerializeField]
    KeySetEditor Right;
    [SerializeField]
    KeySetEditor Down;

    readonly ISubject<KeyMap> value = new ReplaySubject<KeyMap>(1);
    public IObservable<KeyMap> OnValueChangedAsObservable() => value;

    void Start()
    {
        Observable.CombineLatest(
            Center.OnValueChangedAsObservable(),
            Left.OnValueChangedAsObservable(),
            Up.OnValueChangedAsObservable(),
            Right.OnValueChangedAsObservable(),
            Down.OnValueChangedAsObservable(),
            (center, left, up, right, down) => new KeyMap { Center = center, Left = left, Up = up, Right = right, Down = down })
            .ThrottleFrame(1)
            .Subscribe(value)
            .AddTo(this);
    }

    public void Set(KeyMap keyMap)
    {
        Center.Set(keyMap.Center);
        Left.Set(keyMap.Left);
        Up.Set(keyMap.Up);
        Right.Set(keyMap.Right);
        Down.Set(keyMap.Down);
    }
}
