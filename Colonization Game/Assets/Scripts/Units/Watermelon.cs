using System;
using System.Collections;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class Watermelon : MonoBehaviour
    {
        [SerializeField] private float _despawnDelay;
        public event Action<Watermelon> DespawnRequested;
    
        Coroutine _coroutine;
        
        public void Despawn()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(Despawning());
        }
        private IEnumerator Despawning()
        {
            yield return new WaitForSeconds(_despawnDelay);
            DespawnRequested?.Invoke(this);
        }
    }
}