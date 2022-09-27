using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common
{
    public class InputController : MonoBehaviour
    {
        private InputAction _moveAction;

        private void Start()
        {
            var inputBindings = new InputBindings(); 
            _moveAction = inputBindings.Joystick.Move; 
            _moveAction.Enable();
        }

        public Vector2 GetMoveAxis()
        {
            var axis = _moveAction.ReadValue<Vector2>();
            return axis;
        }
        
    }
}