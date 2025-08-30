using System;
using UnityEngine;

namespace BaseSystem
{
    public class BaseStorage : MonoBehaviour
    {
        public event Action<int> ViewRequested;

        private int _watermelonsCount;
        
        public void IncreaseCount()
        {
            _watermelonsCount++;
            ShowInfo();
        }

        public void ShowInfo()
        {
            ViewRequested?.Invoke(_watermelonsCount);
        }
        
        public bool TryDecreaseCount(int value)
        {
            bool isEnough = IsEnough(value);
            
            if (isEnough)
            {
                _watermelonsCount -= value;
                ViewRequested?.Invoke(_watermelonsCount);
            }
            
            return isEnough;
        }
        
        private bool IsEnough(int value)
        {
            return _watermelonsCount >= value;
        }
    }
}