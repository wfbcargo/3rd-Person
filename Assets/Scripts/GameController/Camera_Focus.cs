using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class Camera_Focus : MonoBehaviour
    {
        public GameObject Camera;
        public GameObject LookAt;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Camera.transform.LookAt(LookAt.transform);
        }
    }
}