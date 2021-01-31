﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : GameBehaviour
    {
        public int RunSpeed = 5;
        public int DashPower = 2;
        public float TurnSmooth = 15f;

        private InputMaster.GameplayControlsActions gameplayControls;
        private Rigidbody _body;

        private Quaternion _targetRotation;

        private void Awake()
        {
            gameplayControls = new InputMaster().GameplayControls;
            if (IsMine)
            {
                gameplayControls.Jump.performed += OnJumpPerformed;
                _body = GetComponent<Rigidbody>();
            }
            //Só ativa camera follow se for mine!
            RecursiveFindChild(transform, "CameraFollow").gameObject.SetActive(IsMine);
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("Jump!");
            //TODO
        }

        private void OnEnable()
        {
            if (IsMine)
            {
                gameplayControls.Enable();
            }
        }

        private void OnDisable()
        {
            if (IsMine)
            {
                gameplayControls.Disable();
            }
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
            if (targetDir.magnitude > 0.1f)
            {
                _body.AddForce(_targetRotation * Vector3.forward * RunSpeed * 10 * Time.deltaTime, ForceMode.VelocityChange);
            }
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

        public override void OnGameEnd(string winner)
        {
            if (IsMine)
            {
                gameplayControls.Disable();
            }
        }


    }
}
