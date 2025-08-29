using TMPro;
using UnityEngine;

namespace BaseSystem
{
    public class BaseStorageViewer : MonoBehaviour
    {
        [SerializeField] private string _mainText;
        [SerializeField] private TextMeshProUGUI _textField;
        
        private BaseStorage _currentStorage;

        private void OnDisable()
        {
            StopShowingInfo();
        }
        
        public void SetStorage(BaseStorage storage)
        {
            _currentStorage = storage;
            StartShowingInfo();
        }
        
        public void StopShowingInfo()
        {
            if (_currentStorage != null)
            {
                _currentStorage.ViewRequested -= ChangeText;
            }
        }
        
        private void StartShowingInfo()
        {
            StopShowingInfo();
            _currentStorage.ViewRequested += ChangeText;
        }
        
        private void ChangeText(int count)
        {
            _textField.text = _mainText + count.ToString();
        }
    }
}
