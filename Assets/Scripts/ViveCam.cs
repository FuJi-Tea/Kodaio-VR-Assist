using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveCam : MonoBehaviour
{
    [SerializeField]
    private bool _undistorted = true;

    [SerializeField]
    private bool _cropped = true;

    [SerializeField]
    private bool _followTrakking = false;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Material _material;

    #region ### MonoBehaviour ###
    private void OnEnable()
    {
        EnableSteamVRCamera();
    }

    private void OnDisable()
    {
        DisableSteamVRCamera();
    }

    private void Update()
    {
        UpdateCameraTexture();
    }
    #endregion ### MonoBehaviour ###

    private void UpdateCameraTexture()
    {
        var source = Valve.VR.SteamVR_TrackedCamera.Source(_undistorted);
        var texture = source.texture;

        if (texture == null)
        {
            return;
        }

        _material.mainTexture = texture;

        float aspect = (float)texture.width / texture.height;

        if (_cropped)
        {
            var bounds = source.frameBounds;
            _material.mainTextureOffset = new Vector2(bounds.uMin, bounds.vMin);

            float du = bounds.uMax - bounds.uMin;
            float dv = bounds.vMax - bounds.vMin;

            _material.mainTextureScale = new Vector2(du, dv);

            aspect *= Mathf.Abs(du / dv);
        }
        else
        {
            _material.mainTextureOffset = Vector2.zero;
            _material.mainTextureScale = new Vector2(1f, -1f);
        }

        _target.localScale = new Vector3(1f, 1f / aspect, 1);

        if (_followTrakking)
        {
            if (source.hasTracking)
            {
                var t = source.transform;
                _target.localPosition = t.pos;
                _target.localRotation = t.rot;
            }
        }
    }

    private void EnableSteamVRCamera()
    {
        var source = Valve.VR.SteamVR_TrackedCamera.Source(_undistorted);
        source.Acquire();

        // カメラが認識されていなかったらdisableにする
        if (!source.hasCamera)
        {
            enabled = false;
        }
    }

    private void DisableSteamVRCamera()
    {
        _material.mainTexture = null;

        var source = Valve.VR.SteamVR_TrackedCamera.Source(_undistorted);
        source.Release();
    }
}