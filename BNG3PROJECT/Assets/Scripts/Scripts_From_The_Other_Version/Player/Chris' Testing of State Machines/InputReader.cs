
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInput;


namespace Platformer
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Platformer/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        // Start is called before the first frame update



        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };

        PlayerInput inputActions;

        public Vector3 Direction => (Vector3)inputActions.Player.Move.ReadValue<Vector2>();

        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInput();
                inputActions.Player.SetCallbacks(instance: this);
            }
            inputActions.Enable();

        }
        public void EnablePlayerActions()
        {
            inputActions.Enable();
        }
        public void OnFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            
        }

        public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }
        bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
        public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Move.Invoke(arg0: context.ReadValue<Vector2>());
        }
    }
}
