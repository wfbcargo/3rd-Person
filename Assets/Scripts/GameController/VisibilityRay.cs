using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class VisibilityRay : MonoBehaviour
    {
        public Transform ToPoint;

        private GameObject _lastHitObject;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var ray = new Ray(transform.position, ToPoint.position - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, ToPoint.position)))
            {
                var hitObject = hit.collider.gameObject;
                if (hitObject != _lastHitObject)
                {
                    if (_lastHitObject != null)
                    {
                        _lastHitObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _lastHitObject = hitObject;
                    _lastHitObject.GetComponent<MeshRenderer>().enabled = false;

                }
            }
            else if (_lastHitObject != null)
            {
                _lastHitObject.GetComponent<MeshRenderer>().enabled = true;
                _lastHitObject = null;
            }
        }
    }
}
