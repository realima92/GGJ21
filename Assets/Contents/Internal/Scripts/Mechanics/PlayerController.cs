using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public int RunSpeed = 5;
        public int DashPower = 2;
        public float TurnSmooth = 15f;

        private InputMaster.GameplayControlsActions gameplayControls;
        private Rigidbody _body;
        private Camera _cam;

        private Quaternion _targetRotation;

        private void Awake()
        {
            gameplayControls = new InputMaster().GameplayControls;
            _cam = Camera.main;
            _body = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            gameplayControls.Enable();
        }

        private void OnDisable()
        {
            gameplayControls.Disable();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Updates
            Movement(gameplayControls.Movement.ReadValue<Vector2>());
        }

        private void Movement(Vector2 move)
        {
            Vector3 targetDir = new Vector3(move.x, 0f, move.y);
            targetDir = Camera.main.transform.TransformDirection(targetDir);
            targetDir.y = 0f;
            HandleRotation(targetDir);
            HandleMovement(targetDir);
        }

        private void HandleMovement(Vector3 targetDir)
        {
            _body.MovePosition(transform.position + (targetDir * RunSpeed * Time.deltaTime));
        }

        private void HandleRotation(Vector3 targetDir)
        {
            if (targetDir.magnitude > 0.1f)
            {
                _targetRotation = Quaternion.LookRotation(targetDir, Vector3.up); 
                Quaternion newRotation = Quaternion.Lerp(_body.rotation, _targetRotation, TurnSmooth * Time.deltaTime);
                _body.MoveRotation(newRotation);
            }
        }
    }
}
