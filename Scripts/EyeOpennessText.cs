using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViveSR.anipal.Eye
{
    public class EyeOpennessText : MonoBehaviour
    {
        private float openness = 0;
        public enum Eye
        {
            left, right
        }
        [SerializeField] private Eye eye;
        string eyeName;
        private EyeIndex eyeIndex;



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

            if (eye == Eye.left)
            {
                eyeName = "Left : ";
                eyeIndex = EyeIndex.LEFT;
            }
            else
            {
                eyeName = "Right : ";
                eyeIndex = EyeIndex.RIGHT;
            }
            SRanipal_Eye.GetEyeOpenness(eyeIndex, out openness);

            this.GetComponent<Text>().text = eyeName + openness.ToString("0.000");
        }
    }

}
