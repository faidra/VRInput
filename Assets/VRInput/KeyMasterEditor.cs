using UnityEngine;
using System;
using UniRx;

public class KeyMasterEditor : MonoBehaviour
{
    [SerializeField]
    KeyMapEditor Off;
    [SerializeField]
    KeyMapEditor Center;
    [SerializeField]
    KeyMapEditor Left;
    [SerializeField]
    KeyMapEditor Up;
    [SerializeField]
    KeyMapEditor Right;
    [SerializeField]
    KeyMapEditor Down;

    readonly ISubject<KeyMaster> value = new ReplaySubject<KeyMaster>(1);
    public IObservable<KeyMaster> OnValueChangedAsObservable() => value;

    void Start()
    {
        Observable.CombineLatest(
            Off.OnValueChangedAsObservable(),
            Center.OnValueChangedAsObservable(),
            Left.OnValueChangedAsObservable(),
            Up.OnValueChangedAsObservable(),
            Right.OnValueChangedAsObservable(),
            Down.OnValueChangedAsObservable(),
            (off, center, left, up, right, down) => new KeyMaster { Off = off, Center = center, Left = left, Up = up, Right = right, Down = down })
            .ThrottleFrame(1)
            .Subscribe(value)
            .AddTo(this);
    }

    public void Set(KeyMaster keyMaster)
    {
        Off.Set(keyMaster.Off);
        Center.Set(keyMaster.Center);
        Left.Set(keyMaster.Left);
        Up.Set(keyMaster.Up);
        Right.Set(keyMaster.Right);
        Down.Set(keyMaster.Down);
    }
}
