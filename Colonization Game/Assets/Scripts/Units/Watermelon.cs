using System;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class Watermelon : MonoBehaviour
    {
        public bool IsGrabbed { get; private set; }
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Grab()
        {
            _rigidbody.isKinematic = true;
            IsGrabbed = true;
        }

        public void Ungrab()
        {
            _rigidbody.isKinematic = false;
            transform.SetParent(null);
        }
        
        public void ResetParameters()
        {
            IsGrabbed = false;
        }
    }
}