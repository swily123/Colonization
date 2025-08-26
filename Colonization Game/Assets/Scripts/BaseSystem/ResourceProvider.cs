using System.Collections.Generic;
using System.Linq;
using SpawnerSystem;
using Units;
using UnityEngine;

namespace BaseSystem
{
    public class ResourceProvider : MonoBehaviour
    {
        [SerializeField] private SpawnerWatermelons _spawnerWatermelons;
        
        private readonly List<Watermelon> _allWatermelons = new();
        private readonly List<Watermelon> _takenWatermelons = new();

        private void OnEnable()
        {
            _spawnerWatermelons.MelonSpawned += RegisterWatermelon;
        }

        private void OnDisable()
        {
            _spawnerWatermelons.MelonSpawned -= RegisterWatermelon;
        }

        public Watermelon GetFreeWatermelon()
        {
            return _allWatermelons.Except(_takenWatermelons).FirstOrDefault();
        }

        public void MarkAsTaken(Watermelon melon)
        {
            if (_allWatermelons.Contains(melon) && _takenWatermelons.Contains(melon) == false)
            {
                _takenWatermelons.Add(melon);
            }
        }
        
        private void RegisterWatermelon(Watermelon melon)
        {
            if (_allWatermelons.Contains(melon) == false)
            {
                _allWatermelons.Add(melon);
            }
            
            _takenWatermelons.Remove(melon);
        }
    }
}
