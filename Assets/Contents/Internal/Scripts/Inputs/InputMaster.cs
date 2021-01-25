// GENERATED AUTOMATICALLY FROM 'Assets/Contents/Internal/Scripts/Inputs/Default Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Default Input"",
    ""maps"": [
        {
            ""name"": ""Gameplay Controls"",
            ""id"": ""8e5de5f8-1098-4a30-9104-bffa5bb4cb1a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""2905b0c6-e003-46d0-8187-0c03f4f28473"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a138781b-d88e-4e3c-bdd2-57f3ee9eeae3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""1d5564b0-3c41-48f3-a816-a9230a6e8466"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TogglePause"",
                    ""type"": ""Button"",
                    ""id"": ""1e122720-bb6a-4aad-a8f8-3be5b787a7ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD [Keyboard]"",
                    ""id"": ""3ac2e630-e6a9-480b-aaaa-0d3da5087294"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""38e509fb-8159-4591-b24e-b6a7cf882e46"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""2c5e6d1d-f430-4db8-a9b6-5ed6ecc9c7ce"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Sides [Gamepad]"",
                    ""id"": ""5518e7db-9019-458c-b88b-0fffad44fdc9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9a273d96-d4ff-4ff6-a6bf-c2b78c89b03a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0f159144-3d18-4a55-9716-1aa85d17c90e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""df98bd05-a27e-4e07-9a00-de6131706cbc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df91211a-a880-4951-a114-7e9aeb0a70e3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57877b1c-11f9-4fb4-b7d7-d07c5ebf6e97"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e3678b7-85c5-4508-b76d-5b6da777e0ee"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c02300af-62c6-488d-8bfa-5fc0b9a18e2b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c523110-1e31-45cc-9794-d16e27ab9f02"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu Controls"",
            ""id"": ""b090d508-9c8e-40c5-9e59-a07745e8a1cd"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""090c8eab-2dc3-47b9-8807-bac6be2cf8fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""25766300-8357-4556-ad70-c1b94e5107e3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay Controls
        m_GameplayControls = asset.FindActionMap("Gameplay Controls", throwIfNotFound: true);
        m_GameplayControls_Movement = m_GameplayControls.FindAction("Movement", throwIfNotFound: true);
        m_GameplayControls_Jump = m_GameplayControls.FindAction("Jump", throwIfNotFound: true);
        m_GameplayControls_Crouch = m_GameplayControls.FindAction("Crouch", throwIfNotFound: true);
        m_GameplayControls_TogglePause = m_GameplayControls.FindAction("TogglePause", throwIfNotFound: true);
        // Menu Controls
        m_MenuControls = asset.FindActionMap("Menu Controls", throwIfNotFound: true);
        m_MenuControls_Click = m_MenuControls.FindAction("Click", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay Controls
    private readonly InputActionMap m_GameplayControls;
    private IGameplayControlsActions m_GameplayControlsActionsCallbackInterface;
    private readonly InputAction m_GameplayControls_Movement;
    private readonly InputAction m_GameplayControls_Jump;
    private readonly InputAction m_GameplayControls_Crouch;
    private readonly InputAction m_GameplayControls_TogglePause;
    public struct GameplayControlsActions
    {
        private @InputMaster m_Wrapper;
        public GameplayControlsActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_GameplayControls_Movement;
        public InputAction @Jump => m_Wrapper.m_GameplayControls_Jump;
        public InputAction @Crouch => m_Wrapper.m_GameplayControls_Crouch;
        public InputAction @TogglePause => m_Wrapper.m_GameplayControls_TogglePause;
        public InputActionMap Get() { return m_Wrapper.m_GameplayControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayControlsActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayControlsActions instance)
        {
            if (m_Wrapper.m_GameplayControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCrouch;
                @TogglePause.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnTogglePause;
                @TogglePause.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnTogglePause;
                @TogglePause.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnTogglePause;
            }
            m_Wrapper.m_GameplayControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @TogglePause.started += instance.OnTogglePause;
                @TogglePause.performed += instance.OnTogglePause;
                @TogglePause.canceled += instance.OnTogglePause;
            }
        }
    }
    public GameplayControlsActions @GameplayControls => new GameplayControlsActions(this);

    // Menu Controls
    private readonly InputActionMap m_MenuControls;
    private IMenuControlsActions m_MenuControlsActionsCallbackInterface;
    private readonly InputAction m_MenuControls_Click;
    public struct MenuControlsActions
    {
        private @InputMaster m_Wrapper;
        public MenuControlsActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_MenuControls_Click;
        public InputActionMap Get() { return m_Wrapper.m_MenuControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuControlsActions instance)
        {
            if (m_Wrapper.m_MenuControlsActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_MenuControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public MenuControlsActions @MenuControls => new MenuControlsActions(this);
    public interface IGameplayControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnTogglePause(InputAction.CallbackContext context);
    }
    public interface IMenuControlsActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
}
