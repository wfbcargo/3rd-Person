using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class FocusTarget : MonoBehaviour
    {
        public Transform origin;
        public Transform lookTarget;

        public float focusDistance = 5f;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var viewDir = origin.forward;
            lookTarget.position = origin.forward * focusDistance + origin.position;
        }
    }
}
