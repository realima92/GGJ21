using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public int RunSpeed = 2;
        public int DashPower = 2;
        public int JumpImpulse = 10;
        public float TurnSmooth = 15f;
        public float MaxSpeed = 3f;
        public LayerMask GroundLayer;

        private InputMaster.GameplayControlsActions gameplayControls;
        private Rigidbody _body;
        private Collider _collider;
        private Quaternion _targetRotation;
        private bool _grounded;

        private void Awake()
        {
            gameplayControls = new InputMaster().GameplayControls;
            gameplayControls.Jump.performed += OnJumpPerformed;
            _body = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();       
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            if (_grounded)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _body.AddForce(Vector3.up * JumpImpulse * 100, ForceMode.Impulse);
            _grounded = false;
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
            if (GroundDetected())
            {
                Landed();
            };

            LimitSpeed();
        }

        private void Movement(Vector2 move)
        {
            Vector3 targetDir = new Vector3(move.x, 0f, move.y);
            targetDir = Camera.main.transform.TransformDirection(targetDir);
            targetDir.y = 0f;
            HandleRotation(targetDir);
            HandleMovement(targetDir);
        }

        private bool GroundDetected()
        {
            return Physics.Raycast(_collider.bounds.center, Vector3.down, _collider.bounds.extents.y, GroundLayer.value);
        }

        private void Landed()
        {
            if (_grounded == false)
            {
                _grounded = true;
            }
        }

        private void LimitSpeed()
        {
            if (_body.velocity.magnitude > MaxSpeed)
            {
                _body.velocity = _body.velocity.normalized * MaxSpeed;
            }
        }

        private void HandleMovement(Vector3 targetDir)
        {
            if (targetDir.magnitude > 0.1f)
            {
                _body.AddForce(_targetRotation * Vector3.forward * RunSpeed * 10 * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        private void HandleRotation(Vector3 targetDir)
        {
            if (targetDir.magnitude > 0.1f && _grounded)
            {
                _targetRotation = Quaternion.LookRotation(targetDir, Vector3.up); 
                Quaternion newRotation = Quaternion.Lerp(_body.rotation, _targetRotation, TurnSmooth * Time.deltaTime);
                _body.MoveRotation(newRotation);
            }
        }
    }
}
