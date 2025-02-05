using UnityEngine;
using ViveSR.anipal.Eye;

public class FocusPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _lookPointerObject;

    [SerializeField]
    private Transform _hmdTransform;

    void Update()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING)
            return;

        var gazeRay = GetGazeRay();

        if (Physics.Raycast(gazeRay, out var hit))
        {
            _lookPointerObject.SetActive(true);
            _lookPointerObject.transform.position = hit.point;
        }
        else
        {
            _lookPointerObject.SetActive(false);
        }
    }

    private Ray GetGazeRay()
    {
        var eyeData = GetCombineSingleEyeData();
        var origin = _hmdTransform.position;
        var gazeDirection = eyeData.gaze_direction_normalized;
        gazeDirection.x *= -1;  // トラッキング情報は右手座標系なので反転させる

        return new Ray(origin, _hmdTransform.rotation * gazeDirection);
    }

    private SingleEyeData GetCombineSingleEyeData()
    {
        SRanipal_Eye.GetVerboseData(out var verboseData);
        return verboseData.combined.eye_data;
    }
}