using BaseSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlagSystem
{
    public class MouseRaycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public bool TryGetBase()
        {
            bool isSuccessful = false;
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.TryGetComponent<Base>(out _))
                {
                    isSuccessful = true;
                }
            }
            
            return isSuccessful;
        }
    }
}