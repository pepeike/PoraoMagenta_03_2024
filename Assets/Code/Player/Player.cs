//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Code/Player/Player.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Player: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""main"",
            ""id"": ""b2a06b20-14cd-427c-9ac8-277430c62116"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""6c6239aa-208c-4558-9b41-4c940e47fa47"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UpDown"",
                    ""type"": ""Value"",
                    ""id"": ""bb51e669-3eb1-44d3-8560-6fdf4abc72c8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bb107274-a880-4a55-8223-68f1c7f089ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""63238ce1-88b5-4ebb-b462-56b268153786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Quack"",
                    ""type"": ""Button"",
                    ""id"": ""81c3cb57-1e05-440f-8c07-f096442692da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""36b5fc57-6f1a-41fa-8de7-d89b41f69f41"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d90893ce-7136-4968-99d0-bc18adaa4247"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""bf4f0c4d-d3f7-4fa2-bda0-817c4b547c7c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpDown"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""817695e6-d5e3-457b-b0c2-c3f1eebf6f5b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d81fe94b-e91c-4cbd-99fe-b12e1dc61426"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""b74f8cd9-53fd-4ddc-8bfb-14f2d1ff667b"",
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
                    ""id"": ""33731094-5b22-40a3-845e-28da47f01256"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""684934aa-758d-490d-8894-6d9d8b02b669"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2f23db18-3122-44bc-9da3-26ab3de57ac8"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // main
        m_main = asset.FindActionMap("main", throwIfNotFound: true);
        m_main_Movement = m_main.FindAction("Movement", throwIfNotFound: true);
        m_main_UpDown = m_main.FindAction("UpDown", throwIfNotFound: true);
        m_main_Jump = m_main.FindAction("Jump", throwIfNotFound: true);
        m_main_Attack = m_main.FindAction("Attack", throwIfNotFound: true);
        m_main_Quack = m_main.FindAction("Quack", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // main
    private readonly InputActionMap m_main;
    private List<IMainActions> m_MainActionsCallbackInterfaces = new List<IMainActions>();
    private readonly InputAction m_main_Movement;
    private readonly InputAction m_main_UpDown;
    private readonly InputAction m_main_Jump;
    private readonly InputAction m_main_Attack;
    private readonly InputAction m_main_Quack;
    public struct MainActions
    {
        private @Player m_Wrapper;
        public MainActions(@Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_main_Movement;
        public InputAction @UpDown => m_Wrapper.m_main_UpDown;
        public InputAction @Jump => m_Wrapper.m_main_Jump;
        public InputAction @Attack => m_Wrapper.m_main_Attack;
        public InputAction @Quack => m_Wrapper.m_main_Quack;
        public InputActionMap Get() { return m_Wrapper.m_main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void AddCallbacks(IMainActions instance)
        {
            if (instance == null || m_Wrapper.m_MainActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @UpDown.started += instance.OnUpDown;
            @UpDown.performed += instance.OnUpDown;
            @UpDown.canceled += instance.OnUpDown;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Quack.started += instance.OnQuack;
            @Quack.performed += instance.OnQuack;
            @Quack.canceled += instance.OnQuack;
        }

        private void UnregisterCallbacks(IMainActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @UpDown.started -= instance.OnUpDown;
            @UpDown.performed -= instance.OnUpDown;
            @UpDown.canceled -= instance.OnUpDown;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Quack.started -= instance.OnQuack;
            @Quack.performed -= instance.OnQuack;
            @Quack.canceled -= instance.OnQuack;
        }

        public void RemoveCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainActions instance)
        {
            foreach (var item in m_Wrapper.m_MainActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainActions @main => new MainActions(this);
    public interface IMainActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnUpDown(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnQuack(InputAction.CallbackContext context);
    }
}
