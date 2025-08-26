using System.Collections;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem
{
    [RequireComponent(typeof(PlayerInput))]
    
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private InputModeSwitcher _inputModeSwitcher;
        [SerializeField] private GameObject _target;
        [SerializeField] private float _speed;
        
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _inputModeSwitcher.SetMapActive(InputMode.Camera, true);
        }

        private void OnDisable()
        {
            _inputModeSwitcher.SetMapActive(InputMode.Camera, false);
        }

        public void OnMoving()
        {
            _coroutine = StartCoroutine(TrackingCursorPosition());
        }

        public void OnStopMoving()
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