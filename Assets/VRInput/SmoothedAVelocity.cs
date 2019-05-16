using UnityEngine;
using Valve.VR;
using System.Collections.Generic;
using System.Linq;

public class SmoothedAVelocity : MonoBehaviour
{
    [SerializeField]
    RawAVelocity RawAVelocity;
    [SerializeField]
    int Frames;

    List<Vector3> vs = new List<Vector3>();

    public Vector3 InputVector; // 正が下、正が右、正が左ひねり

    void Update()
    {
        vs.Add(RawAVelocity.InputVector);
        if (vs.Count > Frames) vs.RemoveAt(0);
        InputVector = new Vector3(vs.Select(v => v.x).Average(), vs.Select(v => v.y).Average(), vs.Select(v => v.z).Average());
    }
}
