using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class WASD_Mover : MonoBehaviour
    {
        public Transform directionReference;
        public float speed = 2.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var xDiff = 0f;
            var yDiff = 0f;
            //implement WASD Movements
            if (Input.GetKey(KeyCode.W))
            {
                yDiff = speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                xDiff = -speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                yDiff = -speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xDiff = speed * Time.deltaTime;
            }

            //move relative to direction character is facing
            var forward = directionReference.forward;
            var right = directionReference.right;
            transform.position += forward * yDiff;
            transform.position += right * xDiff;

        }
    }
}
