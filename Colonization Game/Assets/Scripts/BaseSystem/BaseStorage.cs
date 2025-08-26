using System;
using UnityEngine;

namespace BaseSystem
{
    public class BaseStorage : MonoBehaviour
    {
        [SerializeField] private Base _base;
        
        private int _watermelonsCount;
        public event Action<int> CountChanged;
        
        public void IncreaseCount()
        {
            _watermelonsCount++;
            CountChanged?.Invoke(_watermelonsCount);
        }
    }
}