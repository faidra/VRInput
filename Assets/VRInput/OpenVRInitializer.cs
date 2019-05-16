using UnityEngine;
using Valve.VR;

public class OpenVRInitializer : MonoBehaviour
{
    void Start()
    {
        var openVRError = EVRInitError.None;

        //フレームレートを90fpsにする。(しないと無限に早くなることがある)
        Application.targetFrameRate = 90;

        //OpenVRの初期化
        OpenVR.Init(ref openVRError, EVRApplicationType.VRApplication_Overlay);
        if (openVRError != EVRInitError.None)
        {
            Debug.LogError("OpenVRの初期化に失敗." + openVRError.ToString());
            return;
        }
    }
}