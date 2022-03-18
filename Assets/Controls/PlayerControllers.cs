//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Controls/PlayerControllers.inputactions
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

public partial class @PlayerControllers : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControllers()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControllers"",
    ""maps"": [
        {
            ""name"": ""PlayerController"",
            ""id"": ""c6b46db7-78c3-4242-9773-f434c52a0aaa"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ec8de6a0-b01a-49f4-8b6d-3a3b1bbc789c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Value"",
                    ""id"": ""757f9cd0-0c4f-4e3d-8a6a-688460928afc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pass/SwitchPlayer"",
                    ""type"": ""Value"",
                    ""id"": ""40131ae6-2d4f-4b41-817f-b06df8d22bca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot/Tacle"",
                    ""type"": ""Button"",
                    ""id"": ""8d114f3d-950e-4b60-a113-fad52796d2a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dribble/HeadButt"",
                    ""type"": ""Button"",
                    ""id"": ""8f3d9122-e7f7-40d7-893f-4f63c9219a64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SendObject"",
                    ""type"": ""Button"",
                    ""id"": ""13fc470a-04ea-439a-9746-de22df204468"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2DVectorKeyBoard"",
                    ""id"": ""e7eabb04-ac14-43d9-8091-578279b8cff2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d03c9e39-ca9d-4b9d-a5d1-ba5feaad009b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8cfe2910-fda7-4acf-b666-c32a0e79fc5e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fe97b020-dddc-4bad-a64b-aa6ad736823c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""381cdcc8-68ff-4481-a732-7817cb179f7c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""14fafad4-4591-41bb-a6b8-9732ad406a7e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74308e7a-ea5c-4623-ae6c-a353210f9457"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pass/SwitchPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Vector 2 Modifier [Gamepad]"",
                    ""id"": ""63ced2fa-a93c-409c-bbed-f6cab9f09b8f"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pass/SwitchPlayer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""2101bdd0-3c77-4dd9-975e-afeb54c64efb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pass/SwitchPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""b1464e67-9ae3-4895-8a09-e4bd72af403b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pass/SwitchPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Keyboard]"",
                    ""id"": ""5612c50f-6a52-4d1f-9699-824a589688fe"",
                    ""path"": ""Vector2Modifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""0cdd2e15-5db0-4a16-b83e-4986a3c39c62"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""54b11fd3-8868-4e2e-af15-d51ab6128fc7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""ec11674a-e024-4d25-b992-e5b2915d2299"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""35361311-b67d-4eea-b8ae-81b37e9c64b3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""3af1eb09-66a2-4d74-800d-f66008145eab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Gamepad]"",
                    ""id"": ""28d6b978-e595-47fc-82bb-e4179557e005"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""4af4601e-0b9a-4959-814f-4c29c3278755"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""b4ee6fca-2dac-4010-a8b7-8bf1bc787355"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot/Tacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Keyboard]"",
                    ""id"": ""8de0fcb6-344d-492d-b4c3-d2b4e2059a36"",
                    ""path"": ""Vector2Modifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""4796ebb4-22ec-4016-b5b7-a7cafec1c6b3"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""dd7eec63-1ba7-4346-b373-23e081762e20"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""26d546ed-4e54-45b2-bbb4-cec8546a8ed4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""027e879d-453e-4a0c-a58d-171f27f02149"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""8b996dd5-8fa6-46b4-b974-417d91e4b0ad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Gamepad]"",
                    ""id"": ""2c2b958c-8840-4c7c-8edd-e073cc442c24"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""b4ebb557-bd2d-4bb0-b0af-d296cf4b2813"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""352b17ca-29b4-4ff3-a1de-5ecde2a6587e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Keyboard]"",
                    ""id"": ""0c72b8fd-91a1-4a8c-b351-77377f077474"",
                    ""path"": ""Vector2Modifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""db9c6f2e-0517-4c6c-bf86-4d1c4ba15985"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""907fcba5-060f-41ab-903f-d99e00422ff8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""27f17f28-fe20-4323-994a-38ba951d25a1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""8228d042-8ffe-4bc1-88aa-f382839f47c8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""368c18c3-3568-4fc6-9ee9-f0082f440fc8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Gamepad]"",
                    ""id"": ""dca074ba-a42f-4bd2-8593-dbe3fe458fa8"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""53df03f7-b8fc-4154-9a6a-f15a59923593"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""062e5261-bae3-4d01-9ef4-8e06ed35dfba"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dribble/HeadButt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Keyboard]"",
                    ""id"": ""d2713649-211b-4b3a-aba2-e16d45a1807d"",
                    ""path"": ""Vector2Modifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SendObject"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""02f230a0-de9d-4958-a413-6820c7e93e7c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""72936fff-2433-4376-b87d-da8ac4e8a63e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""be97cd00-0105-4e6e-8817-09c915415460"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""13b19a28-2c0b-43aa-a8bf-aa156913d363"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""056be07b-7872-4843-8550-b31eb31db689"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 2 Modifier [Gamepad]"",
                    ""id"": ""71012e63-25e7-4a3e-9576-be1eab6c4437"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SendObject"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""75e6aa5d-87b7-4f84-8f21-6dd3e0cacc09"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""0e740651-a25a-4a4b-9f1b-f2e0b8e3c218"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SendObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerController
        m_PlayerController = asset.FindActionMap("PlayerController", throwIfNotFound: true);
        m_PlayerController_Move = m_PlayerController.FindAction("Move", throwIfNotFound: true);
        m_PlayerController_Sprint = m_PlayerController.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerController_PassSwitchPlayer = m_PlayerController.FindAction("Pass/SwitchPlayer", throwIfNotFound: true);
        m_PlayerController_ShootTacle = m_PlayerController.FindAction("Shoot/Tacle", throwIfNotFound: true);
        m_PlayerController_DribbleHeadButt = m_PlayerController.FindAction("Dribble/HeadButt", throwIfNotFound: true);
        m_PlayerController_SendObject = m_PlayerController.FindAction("SendObject", throwIfNotFound: true);
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

    // PlayerController
    private readonly InputActionMap m_PlayerController;
    private IPlayerControllerActions m_PlayerControllerActionsCallbackInterface;
    private readonly InputAction m_PlayerController_Move;
    private readonly InputAction m_PlayerController_Sprint;
    private readonly InputAction m_PlayerController_PassSwitchPlayer;
    private readonly InputAction m_PlayerController_ShootTacle;
    private readonly InputAction m_PlayerController_DribbleHeadButt;
    private readonly InputAction m_PlayerController_SendObject;
    public struct PlayerControllerActions
    {
        private @PlayerControllers m_Wrapper;
        public PlayerControllerActions(@PlayerControllers wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerController_Move;
        public InputAction @Sprint => m_Wrapper.m_PlayerController_Sprint;
        public InputAction @PassSwitchPlayer => m_Wrapper.m_PlayerController_PassSwitchPlayer;
        public InputAction @ShootTacle => m_Wrapper.m_PlayerController_ShootTacle;
        public InputAction @DribbleHeadButt => m_Wrapper.m_PlayerController_DribbleHeadButt;
        public InputAction @SendObject => m_Wrapper.m_PlayerController_SendObject;
        public InputActionMap Get() { return m_Wrapper.m_PlayerController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControllerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControllerActions instance)
        {
            if (m_Wrapper.m_PlayerControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSprint;
                @PassSwitchPlayer.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPassSwitchPlayer;
                @PassSwitchPlayer.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPassSwitchPlayer;
                @PassSwitchPlayer.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPassSwitchPlayer;
                @ShootTacle.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShootTacle;
                @ShootTacle.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShootTacle;
                @ShootTacle.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnShootTacle;
                @DribbleHeadButt.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnDribbleHeadButt;
                @DribbleHeadButt.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnDribbleHeadButt;
                @DribbleHeadButt.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnDribbleHeadButt;
                @SendObject.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSendObject;
                @SendObject.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSendObject;
                @SendObject.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSendObject;
            }
            m_Wrapper.m_PlayerControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @PassSwitchPlayer.started += instance.OnPassSwitchPlayer;
                @PassSwitchPlayer.performed += instance.OnPassSwitchPlayer;
                @PassSwitchPlayer.canceled += instance.OnPassSwitchPlayer;
                @ShootTacle.started += instance.OnShootTacle;
                @ShootTacle.performed += instance.OnShootTacle;
                @ShootTacle.canceled += instance.OnShootTacle;
                @DribbleHeadButt.started += instance.OnDribbleHeadButt;
                @DribbleHeadButt.performed += instance.OnDribbleHeadButt;
                @DribbleHeadButt.canceled += instance.OnDribbleHeadButt;
                @SendObject.started += instance.OnSendObject;
                @SendObject.performed += instance.OnSendObject;
                @SendObject.canceled += instance.OnSendObject;
            }
        }
    }
    public PlayerControllerActions @PlayerController => new PlayerControllerActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IPlayerControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnPassSwitchPlayer(InputAction.CallbackContext context);
        void OnShootTacle(InputAction.CallbackContext context);
        void OnDribbleHeadButt(InputAction.CallbackContext context);
        void OnSendObject(InputAction.CallbackContext context);
    }
}
