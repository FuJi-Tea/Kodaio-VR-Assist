using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViveSR.anipal.Eye
{
    public class CalibNeedOrNot : MonoBehaviour
    {
        bool need;
        bool isCalled = true;
        // Start is called before the first frame update
        void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

            if (SRanipal_Eye.IsUserNeedCalibration(ref need) == 0 && isCalled)
            {
                if (need) Debug.Log("Calibration Need");
                else Debug.Log("Calibration not Need");
                isCalled = false;
            }
        }
    }
}
