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
    }
}