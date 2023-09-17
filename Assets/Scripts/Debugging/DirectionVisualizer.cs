using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Debugging
{
    [ExecuteInEditMode]
    public class DirectionVisualizer : MonoBehaviour
    {
        // The length of the arrow, in meters
        public float arrowLength = 1.0F;

        private void Start()
        {
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            var position = transform.position;

            Debug.DrawLine(position, position + transform.forward * arrowLength, Color.blue);
            Debug.DrawLine(position, position + transform.right * arrowLength, Color.red);
        }
    }
}
