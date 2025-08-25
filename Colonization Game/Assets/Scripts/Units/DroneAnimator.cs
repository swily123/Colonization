using System.Collections;
using UnityEngine;

namespace Units
{
    public class DroneAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _light;
        [SerializeField] private float _delayAnimation;
        
        private Coroutine _coroutine;
        
        public Coroutine SetGrabAnimation()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(Grabbing());
            return _coroutine;
        }

        private IEnumerator Grabbing()
        {
            _light.SetActive(true);
            yield return new WaitForSeconds(_delayAnimation);
            _light.SetActive(false);
        }
    }
}