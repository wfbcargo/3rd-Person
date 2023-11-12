using Assets.Scripts.Projectiles;
using Assets.Scripts.TriggerType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameController
{
    [RequireComponent(typeof(CharacterController))]
    public class KeyBinder : MonoBehaviour
    {
        public Transform directionReference;
        public float Speed = 12.0f;
        public Gravity Gravity = null;
        public bool EnableGravity = true;

        public List<Gun> OnLeftClickActions;
        public List<Gun> OnRightClickActions;

        private CharacterController controller;
        private CharacterMover mover;

        private bool stop = false;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();

            if (Gravity == null)
            {
                Gravity = FindObjectOfType<Gravity>();
            }
            mover = new CharacterMover(directionReference, Speed, controller, Gravity, ref EnableGravity);
        }

        // Update is called once per frame
        void Update()
        {
            if (stop)
            {
                return;
            }

            mover.Move(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                OnLeftClick();
            }

            if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
            {
                OnRightClick();
            }
        }

        private void OnLeftClick()
        {
            foreach (var action in OnLeftClickActions)
            {
                action.TriggerAction();
            }
        }

        private void OnRightClick()
        {
            foreach (var action in OnRightClickActions)
            {
                action.TriggerAction();
            }
        }
    }
}
