using System.Collections.Generic;
using UnityEngine;

namespace FlagSystem
{
    public class FlagPreview : MonoBehaviour
    {
        [SerializeField] private ColorChanger _colorChanger;
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private Vector3 _triggerSize;
        
        private readonly List<Collider> _obstaclesInRadius = new();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + Vector3.up * _triggerSize.y/2, _triggerSize);
        }

        public void CheckForObstacles()
        {
            Collider[] hits = Physics.OverlapBox(transform.position + Vector3.up * _triggerSize.y/2, _triggerSize, Quaternion.identity, _obstacleLayer);
            
            _obstaclesInRadius.Clear();
            
            foreach (var col in hits)
            {
                if (_obstaclesInRadius.Contains(col))
                    continue;

                _obstaclesInRadius.Add(col);
            }
            
            ChangeColor();
        }

        private void ChangeColor()
        {
            _colorChanger.SetAllColor(_obstaclesInRadius.Count > 0 ? Color.red : Color.white);
        }

        public bool IsClear()
        {
            return _obstaclesInRadius.Count == 0;
        }
    }
}