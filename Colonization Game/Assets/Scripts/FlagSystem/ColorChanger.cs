using UnityEngine;

namespace FlagSystem
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Material[] _renderers;

        private Color[] _originalColors;

        private void Awake()
        {
            _originalColors = new Color[_renderers.Length];

            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i] != null)
                {
                    _originalColors[i] = _renderers[i].color;
                }
                else
                {
                    _originalColors[i] = Color.white;
                }
            }
        }

        private void SetColor(Material material, Color color, float alpha)
        {
            material.color = color;
            material.color = new Color(color.r, color.g, color.b, alpha);
        }

        [ContextMenu("Make Red")]
        public void MakeRed()
        {
            foreach (Material render in _renderers)
            {
                SetColor(render, Color.red, 0.3f);
            }
        }

        [ContextMenu("Restore Colors")]
        public void RestoreOriginalColor()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                SetColor(_renderers[i], _originalColors[i], 0.1f);
            }
        }
    }
}