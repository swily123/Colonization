using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputModeSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        private void Awake()
        {
            TurnOffAllMaps();
        }

        public void SetMapActive(InputMode mode, bool active)
        {
            string mapName = mode.ToString();
            InputActionMap actionMap = _playerInput.actions.FindActionMap(mapName);

            if (actionMap != null)
            {
                if (active)
                {
                    actionMap.Enable();
                }
                else
                {
                    actionMap.Disable();
                }
            }
            else
            {
                Debug.LogWarning($"Action Map '{mapName}' not found!");
            } 
        }
        
        public void TurnOffAllMaps()
        {
            foreach (InputActionMap map in _playerInput.actions.actionMaps)
            {
                map.Disable();
            }
        }
    }
}
