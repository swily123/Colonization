using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Input.PlayerInput;

namespace BaseSystem
{
    public class BaseClicker : MonoBehaviour
    {
        [SerializeField] private BaseFlagSystem _baseFlagSystem;
        [SerializeField] private MouseRaycaster _mouseRaycaster;
        
        private PlayerInput _playerInput;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
        }
        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.ShowBaseInfo.performed += GetBase;
        }

        private void OnDisable()
        {
            _playerInput.Player.ShowBaseInfo.performed -= GetBase;
            _playerInput.Disable();
        }

        private void GetBase(InputAction.CallbackContext context)
        {
            if (_mouseRaycaster.TryGetBase(out Base clickedBase))
            {
                _baseFlagSystem.ChooseBase(clickedBase);
            }
        }
    }
}