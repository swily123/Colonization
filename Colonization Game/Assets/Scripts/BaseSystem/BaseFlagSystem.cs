using FlagSystem;
using UnityEngine;
using UnityEngine.UI;

namespace BaseSystem
{
    public class BaseFlagSystem : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject _baseInfo;
        [SerializeField] private GameObject _inheritorText;
        [SerializeField] private Button _setFlagButton;
        [SerializeField] private Button _exitButton;

        [Header("Scripts")]
        [SerializeField] private BaseStorageViewer _storageViewer;
        [SerializeField] private FlagPlacer  _flagPlacer;
        
        private Base _currentBase;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(ExitFlagSystem);
            //_exitButton.
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(ExitFlagSystem);
        }

        private void Start()
        {
            _baseInfo.SetActive(false);
        }

        public void ChooseBase(Base chosenBase)
        {
            _currentBase = chosenBase;
            _baseInfo.SetActive(true);

            if (_currentBase.transform.TryGetComponent(out BaseStorage baseStorage))
            {
                _storageViewer.SetStorage(baseStorage);
            }
            
            _currentBase.ShowStorage();
            
            if (_currentBase.HaveInheritor)
            {
                _setFlagButton.onClick.RemoveListener(SetFlag);
                _setFlagButton.gameObject.SetActive(false);
                _inheritorText.SetActive(true);
            }
            else
            {
                _setFlagButton.onClick.AddListener(SetFlag);
                _setFlagButton.gameObject.SetActive(true);
                _inheritorText.SetActive(false);
            }
        }

        private void SetFlag()
        {
            _flagPlacer.StartPlacingFlag();
            _flagPlacer.FlagPlaced += OnFlagPlaced;
        }

        private void OnFlagPlaced()
        {
            _flagPlacer.FlagPlaced -= OnFlagPlaced;
            _currentBase.SetInheritor();
            ExitFlagSystem();
        }
        
        private void ExitFlagSystem()
        {
            _storageViewer.StopShowingInfo();
            _baseInfo.SetActive(false);
            _flagPlacer.StopPlacing();
        }
    }
}