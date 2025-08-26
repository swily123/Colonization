using TMPro;
using UnityEngine;

namespace BaseSystem
{
    public class BaseStorageViewer : MonoBehaviour
    {
        [SerializeField] private BaseStorage _storage;
        [SerializeField] private string _mainText;
        [SerializeField] private TextMeshProUGUI _textField;

        private void OnEnable()
        {
            _storage.CountChanged += ChangeText;
        }

        private void OnDisable()
        {
            _storage.CountChanged -= ChangeText;
        }

        private void Start()
        {
            ChangeText(0);
        }

        private void ChangeText(int count)
        {
            _textField.text = _mainText + count.ToString();
        }
    }
}
