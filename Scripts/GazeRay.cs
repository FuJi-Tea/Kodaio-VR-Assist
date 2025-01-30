using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ViveSR.anipal.Eye
{
    public class GazeRay : MonoBehaviour
    {
        public int LengthOfRay = 25;

        public enum Gaze
        {
            Combine,
            Left,
            Right
        }
        [SerializeField] private Gaze whichEye;
        [SerializeField] private LineRenderer GazeRayRenderer;
        Vector3 GazeOriginLocal, GazeDirectionLocal;
        Vector3 GazeDirection;

        private void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }
            Assert.IsNotNull(GazeRayRenderer);
        }

        private void Update()
        {
            if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

            switch (whichEye)
            {
                case Gaze.Left:
                    if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginLocal, out GazeDirectionLocal)) { }
                    else return;
                    GazeDirection = Camera.main.transform.TransformDirection(GazeDirectionLocal);
                    GazeRayRenderer.SetPosition(0, Camera.main.transform.position /*- Camera.main.transform.up * 0.05f*/
                                            - Camera.main.transform.right * 0.034f);
                    GazeRayRenderer.SetPosition(1, Camera.main.transform.position + GazeDirection * LengthOfRay);
                    break;
                case Gaze.Right:
                    if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginLocal, out GazeDirectionLocal)) { }
                    else return;
                    GazeDirection = Camera.main.transform.TransformDirection(GazeDirectionLocal);
                    GazeRayRenderer.SetPosition(0, Camera.main.transform.position /*- Camera.main.transform.up * 0.05f*/
                                            + Camera.main.transform.right * 0.034f);
                    GazeRayRenderer.SetPosition(1, Camera.main.transform.position + GazeDirection * LengthOfRay);
                    break;
                case Gaze.Combine:
                    if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginLocal, out GazeDirectionLocal)) { }
                    else return;
                    GazeDirection = Camera.main.transform.TransformDirection(GazeDirectionLocal);
                    GazeRayRenderer.SetPosition(0, Camera.main.transform.position /*- Camera.main.transform.up * 0.05f*/  );
                    GazeRayRenderer.SetPosition(1, Camera.main.transform.position + GazeDirection * LengthOfRay);
                    break;
            }
        }
    }

}

