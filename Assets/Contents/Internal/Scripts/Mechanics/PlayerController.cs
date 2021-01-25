using UnityEngine;

namespace Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        private InputMaster.GameplayControlsActions gameplayControls;
        private float _movement;
        private bool _crouchPressed;

        private void Awake()
        {
            gameplayControls = new InputMaster().GameplayControls;
        }

        private void OnEnable()
        {
            gameplayControls.Enable();
        }

        private void OnDisable()
        {
            gameplayControls.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            gameplayControls.Jump.performed += OnJump;
        }

        // Update is called once per frame
        void Update()
        {
            _movement = gameplayControls.Movement.ReadValue<float>();
            _crouchPressed = gameplayControls.Crouch.ReadValue<float>() == 1;
            //Updates
            transform.Translate(new Vector3(_movement * Time.deltaTime, 0));
            if (_crouchPressed)
                Debug.Log("Crouching");
        }

        private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext ev)
        {
            Debug.Log("Jump");
        }
    }
}
