using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    public class SurfaceSticker : MonoBehaviour
    {
        public Transform sticker;

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
            var ray = new Ray(transform.position, transform.up * -1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f))
            {
                var hitObject = hit.collider.gameObject;
                if (hitObject != null)
                {
                    var newStickerPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                    var movedVector = newStickerPosition - sticker.position;
                    transform.position += movedVector;
                    _lastHeight = hit.point.y;
                    _lastPosition = transform.position;
                }
            }
            else
            {
                var terrain = GetClosestTerrain();
                if (terrain == null)
                {
                    return;
                }
                var activeHeight = terrain.SampleHeight(sticker.position);

                var newStickerPosition = new Vector3(transform.position.x, activeHeight, transform.position.z);
                var movedVector = newStickerPosition - sticker.position;

                if (_lastHeight > 0 && (activeHeight - _lastHeight >= maxClimb))
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

        public Terrain GetClosestTerrain()
        {
            var terrains = Terrain.activeTerrains;
            return terrains.ToList().OrderBy(t => Vector3.Distance(t.GetPosition(), transform.position)).FirstOrDefault();
        }
    }
}
