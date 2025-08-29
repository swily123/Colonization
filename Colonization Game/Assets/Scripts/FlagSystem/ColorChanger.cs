using UnityEngine;

namespace FlagSystem
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Material[] _renderers;
        [SerializeField, Range(0, 1)] private float _defaultAlpha;
        
        private void SetColor(Material material, Color color, float alpha)
        {
            material.color = color;
            material.color = new Color(color.r, color.g, color.b, alpha);
        }

        public void SetAllColor(Color color)
        {
            foreach (Material render in _renderers)
            {
                SetColor(render, color, _defaultAlpha);
            }
        }
    }
}