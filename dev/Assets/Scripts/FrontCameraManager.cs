using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FrontCameraManager : MonoBehaviour
{
    public GameObject quad; // Quadをアサイン
    private SteamVR_TrackedCamera.VideoStreamTexture cameraStream;
    private Material quadMaterial;

    public bool useLeftEye = true;  // 左目のカメラ映像を使うか右目を使うか設定

    public RenderTexture croppedTexture; // 切り抜き後のテクスチャ
    public Rect cropRect = new Rect(0.1f, 0.1f, 0.5f, 0.5f);  // 切り抜く領域（左下が(0, 0)）

    public Shader cropShader; // 切り抜き用のシェーダー

    void Start()
    {
        if (quad == null)
        {
            Debug.LogError("Quadがアサインされていません！");
            return;
        }

        // QuadのMaterialを取得
        quadMaterial = quad.GetComponent<Renderer>().material;

        // UVスケールを反転
        quadMaterial.mainTextureScale = new Vector2(1, -1); // 水平方向に反転

        // 使用するカメラストリームを決定
        cameraStream = SteamVR_TrackedCamera.Source(useLeftEye);
        cameraStream.Acquire(); // ストリームをロック

        // 切り抜き用のRenderTextureを作成
        if (croppedTexture == null)
        {
            croppedTexture = new RenderTexture(1024, 1024, 24);  // サイズは適宜調整
        }
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
            // 切り抜き処理を呼び出す
            CropTexture(cameraStream.texture, croppedTexture);

            // 切り抜いた映像をQuadに適用
            quadMaterial.mainTexture = croppedTexture;
        }
        else
        {
            Debug.LogWarning("カメラ映像が取得できていません！");
        }
    }

    // 切り抜き処理
    private void CropTexture(Texture inputTexture, RenderTexture outputTexture)
    {
        // 切り抜き用のマテリアルを作成
        Material cropMaterial = new Material(Shader.Find("Unlit/CropShader"));
        cropMaterial.SetTexture("_MainTex", inputTexture); // 入力テクスチャを設定

        // RectをVector4に変換してシェーダーに渡す
        cropMaterial.SetVector("_CropRect", new Vector4(cropRect.x, cropRect.y, cropRect.width, cropRect.height));

        // 切り抜き処理を行う
        Graphics.Blit(inputTexture, outputTexture, cropMaterial);
    }
}
