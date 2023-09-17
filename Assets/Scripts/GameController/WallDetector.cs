using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class WallDetector : MonoBehaviour
    {
        public Transform Detector;

        public float FrameWidth = 0.5f;
        public float EvacuateSpeed = 20f;
        public float MeasurementDistance = .5f;

        public bool ShowDebug = false;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var position = transform.position;

            var changed = false;

            if (FrontWallDetected())
            {
                position = position - Detector.forward * EvacuateSpeed * Time.deltaTime;
                changed = true;
            }
            else if(BackWallDetected())
            {
                position = position + Detector.forward * EvacuateSpeed * Time.deltaTime;
                changed = true;
            }
            
            if(RightWallDetected())
            {
                position = position - Detector.right * EvacuateSpeed * Time.deltaTime;
                changed = true;
            }
            else if(LeftWallDetected())
            {
                position = position + Detector.right * EvacuateSpeed * Time.deltaTime;
                changed = true;
            }

            if(changed)
            {
                transform.position = position;
            }

            if (ShowDebug)
            {
                Debug.DrawRay(Detector.position - Detector.forward * MeasurementDistance, Detector.forward * (MeasurementDistance + FrameWidth), Color.blue);
                Debug.DrawRay(Detector.position + Detector.forward * (MeasurementDistance), Detector.forward * (MeasurementDistance + FrameWidth) * -1, Color.blue);
                Debug.DrawRay(Detector.position - Detector.right * (MeasurementDistance), Detector.right * (MeasurementDistance + FrameWidth), Color.green);
                Debug.DrawRay(Detector.position + Detector.right * (MeasurementDistance), Detector.right * (MeasurementDistance + FrameWidth) * -1, Color.green);
            }

        }

        private bool FrontWallDetected()
        {
            var ray = new Ray(Detector.position - Detector.forward * MeasurementDistance, Detector.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (MeasurementDistance + FrameWidth)))
            {
                return true;
            }
            return false;
        }

        private bool BackWallDetected()
        {
            var ray = new Ray(Detector.position + Detector.forward * MeasurementDistance, Detector.forward * -1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (MeasurementDistance + FrameWidth)))
            {
                return true;
            }
            return false;
        }

        private bool RightWallDetected()
        {
            var ray = new Ray(Detector.position - Detector.right * MeasurementDistance, Detector.right);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (MeasurementDistance + FrameWidth)))
            {
                return true;
            }
            return false;
        }

        private bool LeftWallDetected()
        {
            var ray = new Ray(Detector.position + Detector.right * MeasurementDistance, Detector.right * -1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (MeasurementDistance + FrameWidth)))
            {
                return true;
            }
            return false;
        }
    }
}
