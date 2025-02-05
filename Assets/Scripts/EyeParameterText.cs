using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViveSR.anipal.Eye
{
    public class EyeParameterText : MonoBehaviour
    {
        EyeParameter parameter;

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

            if (SRanipal_Eye.GetEyeParameter(ref parameter) == ViveSR.Error.WORK)
            {
                this.GetComponent<Text>().text = "sensitive : " + parameter.gaze_ray_parameter.sensitive_factor.ToString("0.000");
            }
        }
    }
}
