using UnityEngine;
using Valve.VR;

public class RawPad : MonoBehaviour
{
    [SerializeField]
    ETrackedControllerRole Controller;

    public Vector2 InputVector;
    public ulong ButtonPressed;

    void Update()
    {
        var openvr = OpenVR.System;
        var deviceIndex = (openvr.GetTrackedDeviceIndexForControllerRole(Controller));
        VRControllerState_t state = default;
        openvr.GetControllerState(deviceIndex, ref state, (uint)System.Runtime.InteropServices.Marshal.SizeOf<VRControllerState_t>());
        InputVector = new Vector2(state.rAxis0.x, state.rAxis0.y);
        ButtonPressed = state.ulButtonPressed;
    }
}
