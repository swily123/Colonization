using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Input.PlayerInput;

namespace CameraSystem
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private float _speed;
        
        private Coroutine _coroutine;
        
        private PlayerInput _playerInput;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.Moving.performed += OnMoving;
            _playerInput.Player.StopMoving.performed += OnStopMoving;
        }

        private void OnDisable()
        {
            _playerInput.Player.Moving.performed -= OnMoving;
            _playerInput.Player.StopMoving.performed -= OnStopMoving;
            _playerInput.Disable();
        }
        
        private void OnMoving(InputAction.CallbackContext context)
        {
            StopMovement();
            _coroutine = StartCoroutine(TrackingCursorPosition());
        }

        private void OnStopMoving(InputAction.CallbackContext context)
        {
            StopMovement();
        }

        private void StopMovement()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        
        private IEnumerator TrackingCursorPosition()
        {
            while (enabled)
            {
                Vector3 delta = Mouse.current.delta.ReadValue();
                Vector3 translation = new Vector3(-delta.x, 0, -delta.y);
                _target.transform.Translate(translation * (_speed * Time.deltaTime), Space.World);
                
                yield return null;
            }
        }
    }
}