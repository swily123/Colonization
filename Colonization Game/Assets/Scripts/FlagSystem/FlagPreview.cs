using System.Collections.Generic;
using UnityEngine;

namespace FlagSystem
{
    public class FlagPreview : MonoBehaviour
    {
        [SerializeField] private ColorChanger _colorChanger;
        [SerializeField] private LayerMask _obstacleLayer;

        private readonly List<Collider> _obstaclesInRadius = new();

        private void Update()
        {
            if (transform.position != Vector3.zero)
            {
                CheckForObstacles();
            }
        }

        private void CheckForObstacles()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 3, _obstacleLayer);
            
            _obstaclesInRadius.Clear();
            
            foreach (var col in hits)
            {
                if (_obstaclesInRadius.Contains(col))
                    continue;

                _obstaclesInRadius.Add(col);
                Debug.Log($"Препятствие рядом: {col.name}");
            }
            
            ChangeColor();
        }

        private void ChangeColor()
        {
            if (_obstaclesInRadius.Count > 0)
            {
                _colorChanger.MakeRed();
            }
            else
            {
                _colorChanger.RestoreOriginalColors();
            }
        }

        public bool IsClear()
        {
            return _obstaclesInRadius.Count == 0;
        }
    }
}