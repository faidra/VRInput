using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;

public class KeySetEditor : MonoBehaviour
{
    [SerializeField]
    Toggle IsTapTrigger;
    [SerializeField]
    InputField Tap;
    [SerializeField]
    InputField Snap;
    [SerializeField]
    InputField Left;
    [SerializeField]
    InputField Up;
    [SerializeField]
    InputField Right;
    [SerializeField]
    InputField Down;

    readonly ISubject<KeySet> value = new ReplaySubject<KeySet>(1);
    public IObservable<KeySet> OnValueChangedAsObservable() => value;

    void Start()
    {
        IsTapTrigger.OnValueChangedAsObservable()
            .Subscribe(ChangeInteractable)
            .AddTo(this);

        Observable.CombineLatest(
            IsTapTrigger.OnValueChangedAsObservable(),
            Tap.OnValueChangedAsObservable(),
            Snap.OnValueChangedAsObservable(),
            Left.OnValueChangedAsObservable(),
            Up.OnValueChangedAsObservable(),
            Right.OnValueChangedAsObservable(),
            Down.OnValueChangedAsObservable(),
            (isTap, tap, snap, left, up, right, down) => new KeySet { IsTapTrigger = isTap, Tap = tap, Snap = snap, Left = left, Up = up, Right = right, Down = down })
            .ThrottleFrame(1)
            .Subscribe(value)
            .AddTo(this);
    }

    public void Set(KeySet keySet)
    {
        IsTapTrigger.isOn = keySet.IsTapTrigger;
        Tap.text = keySet.Tap;
        Snap.text = keySet.Snap;
        Left.text = keySet.Left;
        Up.text = keySet.Up;
        Right.text = keySet.Right;
        Down.text = keySet.Down;
    }

    void ChangeInteractable(bool isTapTrigger)
    {
        Tap.interactable = isTapTrigger;
        Snap.interactable = !isTapTrigger;
        Left.interactable = !isTapTrigger;
        Up.interactable = !isTapTrigger;
        Right.interactable = !isTapTrigger;
        Down.interactable = !isTapTrigger;
    }
}
