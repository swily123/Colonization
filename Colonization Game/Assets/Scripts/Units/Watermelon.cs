using System;
using System.Collections;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class Watermelon : MonoBehaviour
    {
        [SerializeField] private float _despawnDelay;
        public bool WillTaken { get; private set; }
        public event Action<Watermelon> DespawnRequested;
    
        private Rigidbody _rigidbody;
        Coroutine _coroutine;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Assign()
        {
            WillTaken = true;
        }
        
        public void Grab()
        {
            _rigidbody.isKinematic = true;
        }

        public void Ungrab()
        {
            _rigidbody.isKinematic = false;
            transform.SetParent(null);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(Despawning());
        }
        
        public void ResetParameters()
        {
            WillTaken = false;
        }

        private IEnumerator Despawning()
        {
            yield return new WaitForSeconds(_despawnDelay);
            DespawnRequested?.Invoke(this);
        }
    }
}