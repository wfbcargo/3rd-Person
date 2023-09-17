using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class VisibilityRay : MonoBehaviour
    {
        public Transform ToPoint;

        private GameObject _lastHitObject;
        private Material _lastHitMaterial;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, ToPoint.position)))
            {
                var hitObject = hit.collider.gameObject;
                if (hitObject != _lastHitObject)
                {
                    if (_lastHitObject != null)
                    {
                        _lastHitObject.GetComponent<Renderer>().material = _lastHitMaterial;
                    }

                    _lastHitObject = hitObject;
                    _lastHitMaterial = hitObject.GetComponent<Renderer>().material;
                    var newMaterial = new Material(_lastHitMaterial);
                    newMaterial.color = new Color(newMaterial.color.r, newMaterial.color.g, newMaterial.color.b, 0.5f);
                    hitObject.GetComponent<Renderer>().material = newMaterial;
                }
            }
            else if (_lastHitObject != null)
            {
                _lastHitObject.GetComponent<Renderer>().material = _lastHitMaterial;
                _lastHitObject = null;
            }
        }
    }
}
