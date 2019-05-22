using UnityEngine;
using System;
using UniRx;
using UnityEngine.UI;
using System.Linq;

public class ToggleButtons : MonoBehaviour
{
    [Serializable]
    struct ButtonAndGameObject
    {
        public Button Button;
        public GameObject GameObject;
    }
    [SerializeField]
    ButtonAndGameObject[] ButtonAndGameObjects;

    void Start()
    {
        ButtonAndGameObjects
            .Select((bg, i) => bg.Button.OnClickAsObservable().Select(_ => i))
            .Merge()
            .Merge(Observable.Return(0).ObserveOnMainThread(MainThreadDispatchType.LateUpdate)) // すべてのStartを待ってから
            .Subscribe(i =>
        {
            foreach (var bg in ButtonAndGameObjects.WithIndex())
            {
                bg.Item1.Button.interactable = bg.Item2 != i;
                bg.Item1.GameObject.SetActive(bg.Item2 == i);
            }
        })
        .AddTo(this);
    }
}
