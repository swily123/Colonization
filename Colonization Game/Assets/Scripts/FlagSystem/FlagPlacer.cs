using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlagSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class FlagPlacer : MonoBehaviour
    {
        [SerializeField] private InputModeSwitcher _inputModeSwitcher;
        [SerializeField] private MouseRaycaster _mouseRaycaster;

        private void OnEnable()
        {
            _inputModeSwitcher.SetMapActive(InputMode.Station, true);
        }

        private void OnDisable()
        {
            _inputModeSwitcher.SetMapActive(InputMode.Station, false);
        }

        public void OnPlaceFlag()
        {
            if (_mouseRaycaster.TryGetBase())
            {
                Debug.Log("Placing Flag");
            }
        }
    }
}