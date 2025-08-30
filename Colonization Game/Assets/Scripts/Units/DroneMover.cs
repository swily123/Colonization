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
        private Coroutine _controlCoroutine;
        
        public void SetPoint(Vector3 point, Action onArrive)
        {
            Debug.Log("set point");
            LookAtPoint(point);
            StopSelfCoroutine(_controlCoroutine);
            _controlCoroutine = StartCoroutine(ControllingMovement(point, onArrive));
        }

        private void StopSelfCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        private IEnumerator ControllingMovement(Vector3 point, Action onArrive)
        {
            Debug.Log("control point");
            StopSelfCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(Moving(point));

            yield return _moveCoroutine;
            onArrive?.Invoke();
            Debug.Log("stop control moving");
            StopSelfCoroutine(_moveCoroutine);
        }
        
        private IEnumerator Moving(Vector3 point)
        {
            Debug.Log("start moving");
            
            while (IsTouchPoint(point) == false)
            {
                Debug.Log("moving");
                transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);
                
                yield return null;
            }
            
            Debug.Log("no moving");
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
