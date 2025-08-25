using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace BaseSystem
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private List<Drone> _drones;
        [SerializeField] private BaseLocator _baseLocator;
        [SerializeField] private float _delay;
        
        Coroutine _coroutine;
        
        private void Start()
        {
            _coroutine = StartCoroutine(Scanning());
        }

        private IEnumerator Scanning()
        {
            WaitForSeconds wait = new WaitForSeconds(_delay);

            while (enabled)
            {
                Debug.Log(_baseLocator.Scan().Count);
                yield return wait;
            }
        }
    }
}