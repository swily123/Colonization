using BaseSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlagSystem
{
    public class MouseRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _ground; 
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

        public bool TryGetCollisionPointGround(out Vector3 collisionPoint)
        {
            bool isSuccessful = false;
            collisionPoint = Vector3.zero;
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ground))
            {
                isSuccessful = true;
                collisionPoint = hit.point;
            }
            
            return isSuccessful;
        }
    }
}