using System.Collections;
using UnityEngine;

namespace Units
{
    public class DroneAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _light;
        [SerializeField] private float _delayGrabbingAnimation;
        [SerializeField] private float _delayBuildingAnimation;

        private Coroutine _grabbingCoroutine;
        private Coroutine _buildingCoroutine;

        public Coroutine SetGrabAnimation()
        {
            StopAnimationCoroutine(_grabbingCoroutine);
            _grabbingCoroutine = StartCoroutine(Grabbing());
            return _grabbingCoroutine;
        }

        public Coroutine SetBuildAnimation()
        {
            StopAnimationCoroutine(_buildingCoroutine);
            _buildingCoroutine = StartCoroutine(Building());
            return _buildingCoroutine;
        }

        private void StopAnimationCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        private IEnumerator Building()
        {
            yield return new WaitForSeconds(_delayBuildingAnimation);
        }
        
        private IEnumerator Grabbing()
        {
            _light.SetActive(true);
            yield return new WaitForSeconds(_delayGrabbingAnimation);
            _light.SetActive(false);
        }
    }
}