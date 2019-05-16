using UnityEngine;

public class PadState : MonoBehaviour
{
    public enum State
    {
        Off,
        Center,
        Left,
        Up,
        Right,
        Down,
    }

    [SerializeField]
    RawPad RawPad;

    [SerializeField]
    Vector2 Center;
    [SerializeField]
    float CenterSqrRadius = 0.35f;

    public State GetState()
    {
        if (RawPad.InputVector.sqrMagnitude == 0f) return State.Off;
        var diff = RawPad.InputVector - Center;
        if (diff.sqrMagnitude < CenterSqrRadius) return State.Center;
        var t = Mathf.Atan2(diff.y, diff.x);
        if (t < -3f / 4 * Mathf.PI) return State.Left;
        if (t < -1f / 4 * Mathf.PI) return State.Down;
        if (t < 1f / 4 * Mathf.PI) return State.Right;
        if (t < 3f / 4 * Mathf.PI) return State.Up;
        return State.Left;
    }
}
