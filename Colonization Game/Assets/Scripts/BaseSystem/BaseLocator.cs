using System.Collections.Generic;
using SpawnerSystem;
using Units;
using UnityEngine;

namespace BaseSystem
{
    public class BaseLocator : MonoBehaviour
    {
        [SerializeField] private Zoner _zoner;
        
        private Vector3 _scanSize;
        
        private void Start()
        {
            _scanSize = new Vector3(_zoner.XDistance, 2, _zoner.ZDistance);
        }

        public List<Watermelon> Scan()
        {
            List<Watermelon> watermelons = new List<Watermelon>();
            Collider[] hits = Physics.OverlapBox(transform.position, _scanSize, Quaternion.identity);

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out Watermelon watermelon))
                {
                    if (watermelon.IsGrabbed == false)
                    {
                        watermelons.Add(watermelon);
                    }
                }
            }
            
            return watermelons;
        }
    }
}
