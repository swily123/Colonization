using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlagSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class FlagPlacer : MonoBehaviour
    {
        [SerializeField] private MouseRaycaster _mouseRaycaster;
        [SerializeField] private FlagPreview _flagPreviewPrefab;

        private void Start()
        {
            _flagPreviewPrefab.gameObject.SetActive(false);
        }
        
        public void OnPlaceFlag()
        {
            if (_mouseRaycaster.TryGetBase())
            {
                _flagPreviewPrefab.gameObject.SetActive(true);
                StartCoroutine(PlacementFlag());
            }
        }

        private IEnumerator PlacementFlag()
        {
            while (enabled)
            {
                if (_mouseRaycaster.TryGetCollisionPointGround(out Vector3 flagPosition))
                {
                    _flagPreviewPrefab.transform.position = flagPosition;
                }
                
                yield return null;
            }
        }

        private void Check()
        {
            if (_flagPreviewPrefab.IsClear())
            {
                
            }
        }
    }
}