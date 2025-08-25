using System.Collections;
using UnityEngine;

namespace Units
{
    public class DroneGrabber : MonoBehaviour
    {
        [SerializeField] private float _grabbingDelta;
        [SerializeField] private float _grabbingDeltaMax;
        
        private Coroutine _coroutine;
        
        public void Grab(Watermelon watermelon)
        {
            watermelon.transform.SetParent(transform);
            SetMelonKinematic(watermelon, true);
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(Grabbing(watermelon));
        }

        public void Ungrab(Watermelon watermelon)
        {
            watermelon.transform.SetParent(null);
            SetMelonKinematic(watermelon, false);
        }

        private void SetMelonKinematic(Watermelon watermelon, bool state)
        {
            if (watermelon.transform.TryGetComponent(out Rigidbody melonRigidbody))
            {
                melonRigidbody.isKinematic = state;
            }
        }
        
        private IEnumerator Grabbing(Watermelon watermelon)
        {
            Vector3 endPosition = watermelon.transform.position + Vector3.up * _grabbingDeltaMax;
            
            while (watermelon.transform.position != endPosition)
            {
                watermelon.transform.position = Vector3.MoveTowards(watermelon.transform.position, endPosition, _grabbingDelta);
                yield return null;
            }
        }
    }
}
