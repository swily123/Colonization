using TMPro;
using UnityEngine;

namespace BaseSystem
{
    public class BaseStorageViewer : MonoBehaviour
    {
        [SerializeField] private Base _base;
        [SerializeField] private string _mainText;
        [SerializeField] private TextMeshProUGUI _textField;

        private void OnEnable()
        {
            _base.WatermelonsChanged += ChangeText;
        }

        private void OnDisable()
        {
            _base.WatermelonsChanged -= ChangeText;
        }

        private void ChangeText(int count)
        {
            _textField.text = _mainText + count.ToString();
        }
    }
}
