using UnityEngine;
using Valve.VR;
using System.Collections.Generic;
using System.Linq;

public class RawAVelocity : MonoBehaviour
{
    [SerializeField]
    ETrackedControllerRole Controller;

    [SerializeField]
    int Means;

    List<Vector3> vs = new List<Vector3>();

    public Vector3 InputVector; // 正が下、正が右、正が左ひねり

    void Update()
    {
        vs.Add(GetAVelocity());
        if (vs.Count > Means) vs.RemoveAt(0);
        InputVector = new Vector3(vs.Select(v => v.x).Average(), vs.Select(v => v.y).Average(), vs.Select(v => v.z).Average());
    }

    Vector3 GetAVelocity()
    {
        var openvr = OpenVR.System;
        var deviceIndex = (openvr.GetTrackedDeviceIndexForControllerRole(Controller));
        if (deviceIndex < 0 || OpenVR.k_unMaxTrackedDeviceCount <= deviceIndex) return Vector3.zero;
        var poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
        openvr.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseRawAndUncalibrated, 0f, poses);
        var pose = poses[deviceIndex];
        var transform = new SteamVR_Utils.RigidTransform(pose.mDeviceToAbsoluteTracking);
        var v = new Vector3(pose.vVelocity.v0, pose.vVelocity.v1, pose.vVelocity.v2);
        var rawVel = new Vector3(-pose.vAngularVelocity.v0, -pose.vAngularVelocity.v1, pose.vAngularVelocity.v2); // 座標系変換
        transform.Inverse();
        return transform.rot * rawVel;
    }
}
