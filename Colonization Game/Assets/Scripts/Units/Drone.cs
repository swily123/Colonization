using System;
using System.Collections;
using UnityEngine;

namespace Units
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private DroneMover _mover;
        [SerializeField] private DroneAnimator _animator;
        [SerializeField] private DroneGrabber _grabber;
        
        public bool IsOnMission { get; private set; }
        public event Action MissionCompleted;
        
        private Coroutine _watermelonCoroutine;
        private Coroutine _buildCoroutine;
        private Watermelon _watermelonToGrab;
        private Vector3 _basePosition;

        private void Start()
        {
            _basePosition = transform.position;
        }

        public void GoToWatermelon(Watermelon watermelon)
        {
            _watermelonToGrab = watermelon;
            IsOnMission = true;
            
            Vector3 watermelonPosition = _watermelonToGrab.transform.position;
            Debug.Log(watermelonPosition + "watermelon");
            watermelonPosition.y = transform.position.y;
            
            _mover.SetPoint(watermelonPosition, OnArrivedAtWatermelon);
        }

        public void GoToPoint(Vector3 point)
        {
            Debug.Log("Going to the point");
            IsOnMission = true;
            //point.y = transform.position.y;
            _mover.SetPoint(point, OnArrivedAtPoint);
        }
        
        private IEnumerator WaitAnimationAndGoBase(Coroutine actionCoroutine)
        {
            yield return actionCoroutine;
            _mover.SetPoint(_basePosition, OnArrivedAtBase);
        }
        
        private void OnArrivedAtPoint()
        {
            StopActionCoroutine(_buildCoroutine);
            _buildCoroutine = StartCoroutine(WaitAnimationAndGoBase(_animator.SetBuildAnimation()));
        }
        
        private void OnArrivedAtWatermelon()
        {
            StopActionCoroutine(_watermelonCoroutine);
            _grabber.Grab(_watermelonToGrab);
            _watermelonCoroutine = StartCoroutine(WaitAnimationAndGoBase(_animator.SetGrabAnimation()));
        }
        
        private void OnArrivedAtBase()
        {
            _grabber.Ungrab(_watermelonToGrab);
            _watermelonToGrab.Despawn();
            IsOnMission = false;
            MissionCompleted?.Invoke();
        }
        
        private void StopActionCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }
}
