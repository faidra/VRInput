using UnityEngine;
using System;

public class SnapEvent : MonoBehaviour
{
    public event Action Up, Down, Left, Right, RRoll, LRoll;

    [SerializeField]
    SmoothedAVelocity AVelocity;

    [SerializeField]
    float LRollThreshold;
    [SerializeField]
    float RRollThreshold;
    [SerializeField]
    float LeftThreshold;
    [SerializeField]
    float RightThreshold;
    [SerializeField]
    float UpThreshold;
    [SerializeField]
    float DownThreshold;
    [SerializeField]
    float RollStabilizerRate;

    enum State
    {
        None, Up, Down, Left, Right, RRoll, LRoll
    }
    State current = State.None;

    void Update()
    {
        var v = AVelocity.InputVector;
        var stabilize = RollStabilizerRate * Mathf.Abs(v.z); // Roll時に上下が反応してしまうので、Rollがあれば上下判定を厳しくする
        switch (current)
        {
            case State.None:
                if (v.x < -UpThreshold - stabilize)
                {
                    Up?.Invoke();
                    current = State.Up;
                }
                else if (v.x > DownThreshold + stabilize)
                {
                    Down?.Invoke();
                    current = State.Down;
                }
                if (v.y < -LeftThreshold)
                {
                    Left?.Invoke();
                    current = State.Left;
                }
                else if (v.y > RightThreshold)
                {
                    Right?.Invoke();
                    current = State.Right;
                }
                if (v.z < -RRollThreshold)
                {
                    RRoll?.Invoke();
                    current = State.RRoll;
                }
                else if (v.z > LRollThreshold)
                {
                    LRoll?.Invoke();
                    current = State.LRoll;
                }
                break;
            case State.Up:
                if (v.x > -UpThreshold) current = State.None;
                break;
            case State.Down:
                if (v.x < DownThreshold) current = State.None;
                break;
            case State.Left:
                if (v.y > -LeftThreshold) current = State.None;
                break;
            case State.Right:
                if (v.y < RightThreshold) current = State.None;
                break;
            case State.RRoll:
                if (v.z > -RRollThreshold) current = State.None;
                break;
            case State.LRoll:
                if (v.z < LRollThreshold) current = State.None;
                break;
            default:
                break;
        }
    }
}
