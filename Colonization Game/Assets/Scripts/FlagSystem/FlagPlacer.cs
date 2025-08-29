using System;
using System.Collections;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using PlayerInput = Input.PlayerInput;

namespace FlagSystem
{
    public class FlagPlacer : MonoBehaviour
    {
        [SerializeField] private MouseRaycaster _mouseRaycaster;
        [SerializeField] private FlagPreview _flagPreviewPrefab;
        [SerializeField] private Flag _originalFlag;
        
        public event Action FlagPlaced;
        
        private PlayerInput _playerInput;
        private Coroutine _coroutine;
        private bool _inPlacingMode;

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Player.PlaceFlag.performed += CheckOnZone;
        }

        private void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.Player.PlaceFlag.performed -= CheckOnZone;
        }

        private void Start()
        {
            _flagPreviewPrefab.gameObject.SetActive(false);
        }

        public void StartPlacingFlag()
        {
            StopPlacingCoroutine();
            _coroutine = StartCoroutine(Placing());
            _flagPreviewPrefab.gameObject.SetActive(true);
            _inPlacingMode = true;
        }

        public void StopPlacing()
        {
            StopPlacingCoroutine();
            _flagPreviewPrefab.gameObject.SetActive(false);
            _inPlacingMode = false;
        }

        private void StopPlacingCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator Placing()
        {
            while (enabled)
            {
                if (_mouseRaycaster.TryGetCollisionPointGround(out Vector3 flagPosition))
                {
                    _flagPreviewPrefab.transform.position = flagPosition;
                    _flagPreviewPrefab.CheckForObstacles();
                }

                yield return null;
            }
        }

        private void CheckOnZone(InputAction.CallbackContext context)
        {
            StartCoroutine(PlaceFlag());
        }

        private IEnumerator PlaceFlag()
        {
            yield return null;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                yield break;
            }
            
            if (_inPlacingMode && _flagPreviewPrefab.IsClear())
            {
                if (_mouseRaycaster.TryGetCollisionPointGround(out Vector3 flagPosition))
                {
                    Instantiate(_originalFlag, flagPosition, Quaternion.identity);
                    StopPlacing();
                    FlagPlaced?.Invoke();
                }
            }
        }
    }
}