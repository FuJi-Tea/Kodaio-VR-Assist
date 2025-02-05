using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        if (cameraTransform != null)
        {
            // カメラの位置と回転に追従
            transform.position = cameraTransform.position;
            transform.rotation = cameraTransform.rotation;
        }
    }
}

