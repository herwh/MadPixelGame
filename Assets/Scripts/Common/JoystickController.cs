using UnityEngine;
using UnityEngine.InputSystem;

namespace Common
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private InputAction _touchPressAction;
        private InputAction _touchPositionAction;
      
        private void Start()
        {
            var inputBindings = new InputBindings();
            var joystickActions = inputBindings.Joystick;
            _touchPressAction = joystickActions.TouchPress;
            _touchPositionAction = joystickActions.TouchPosition;
            
            _touchPressAction.performed+= StartTouchPress;
            _touchPressAction.canceled+= StopTouchPress;
            
            _touchPressAction.Enable();
            _touchPositionAction.Enable();
        }
        
        private void StartTouchPress(InputAction.CallbackContext obj)
        {
            var touchPosition = _touchPositionAction.ReadValue<Vector2>();
            transform.position = touchPosition;
            _canvasGroup.alpha = 1;
        }
        
        private void StopTouchPress(InputAction.CallbackContext obj)
        {
            _canvasGroup.alpha = 0;
        }

        
    }
}
