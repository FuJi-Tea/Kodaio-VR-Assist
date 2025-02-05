using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool rotate = true;
    [SerializeField] private Camera myCamera;
    void Start()
    {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
        if (!rotate) UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(myCamera, true);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
}
