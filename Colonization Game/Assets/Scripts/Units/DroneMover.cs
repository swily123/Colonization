using System;
using System.Collections;
using UnityEngine;

namespace Units
{
    public class DroneMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _minDistanceToPoint;

        private Coroutine _moveCoroutine;
        
        public void SetPoint(Vector3 point, Action onArrive)
        {
            LookAtPoint(point);
            StartCoroutine(ControllingMovement(point, onArrive));
        }

        private void StopMoving()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
        }

        private IEnumerator ControllingMovement(Vector3 point, Action onArrive)
        {
            StopMoving();
            _moveCoroutine = StartCoroutine(Moving());

            yield return new WaitUntil(() => IsTouchPoint(point));

            onArrive?.Invoke();
            
            StopMoving();
        }
        
        private IEnumerator Moving()
        {
            while (enabled)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * _speed));
                
                yield return null;
            }
        }
        
        private bool IsTouchPoint(Vector3 point)
        {
            Vector3 position = transform.position;
            position.y = 0;
            
            Vector3 pointPosition = point;
            pointPosition.y = 0;

            float distance = (pointPosition - position).sqrMagnitude;

            bool isTouch = distance <= _minDistanceToPoint * _minDistanceToPoint;
            return isTouch;
        }

        private void LookAtPoint(Vector3 point)
        {
            Vector3 direction = (point - transform.position).normalized;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
