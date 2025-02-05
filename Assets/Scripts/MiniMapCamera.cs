using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MiniMapCamera : MonoBehaviour
{
    public GameObject quad; // Quadをアサイン
    private SteamVR_TrackedCamera.VideoStreamTexture cameraStream;
    private Material quadMaterial;

    public bool useLeftEye = true;  // 左目のカメラ映像を使うか右目を使うか設定

    void Start()
    {
        if (quad == null)
        {
            Debug.LogError("Quadがアサインされていません！");
            return;
        }

        // QuadのMaterialを取得
        quadMaterial = quad.GetComponent<Renderer>().material;

        // 使用するカメラストリームを決定
        cameraStream = SteamVR_TrackedCamera.Source(useLeftEye);
        cameraStream.Acquire(); // ストリームをロック
    }

    void OnDestroy()
    {
        if (cameraStream != null)
        {
            cameraStream.Release(); // ストリームを解放
        }
    }

    void Update()
    {
        if (cameraStream != null && cameraStream.texture != null)
        {
            // Quadのマテリアルにカメラ映像を割り当て
            quadMaterial.mainTexture = cameraStream.texture;
        }
        else
        {
            Debug.LogWarning("カメラ映像が取得できていません！");
        }
    }
}



