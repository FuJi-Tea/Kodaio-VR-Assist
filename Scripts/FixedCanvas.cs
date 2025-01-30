using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCanvas : MonoBehaviour
{
    public Transform cameraTransform; // メインカメラを設定

    void Update()
    {
        // カメラと同じ位置と回転にCanvasを固定
        transform.position = cameraTransform.position + cameraTransform.forward * 2f; // 距離を調整
        transform.rotation = cameraTransform.rotation;
    }
}
