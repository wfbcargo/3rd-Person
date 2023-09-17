using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class SurfaceSticker : MonoBehaviour
    {
        public Transform sticker;
        public Terrain terrain;

        public float maxClimb = 0.01f;

        private float _lastHeight = 0f;
        private Vector3 _lastPosition = Vector3.zero;
        // Start is called before the first frame update
        void Start()
        {
            _lastPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            var activeHeight = terrain.SampleHeight(sticker.position);

            var newStickerPosition = new Vector3(transform.position.x, activeHeight, transform.position.z);
            var movedVector = newStickerPosition - sticker.position;

            if(_lastHeight > 0 && (activeHeight - _lastHeight >= maxClimb))
            {
                transform.position = _lastPosition;
            }
            else
            {
                transform.position += movedVector;
                _lastHeight = activeHeight;
                _lastPosition = transform.position;
            }
        }
    }
}
