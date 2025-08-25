using System;
using System.Collections;
using UnityEngine;

namespace Units
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private Transform _basePosition;
        [SerializeField] private DroneMover _mover;
        [SerializeField] private DroneAnimator _animator;
        [SerializeField] private DroneGrabber _grabber;
        
        public bool IsOnMission { get; private set; }
        public event Action MissionCompleted;
        
        private Coroutine _coroutine;
        private Watermelon _watermelonToGrab;
        
        private void Start()
        {
            MissionComplete();
        }

        public void GoToPoint(Watermelon watermelon)
        {
            _watermelonToGrab = watermelon;
            IsOnMission = true;
            _mover.SetPoint(watermelon.transform.position, OnArrivedAtPoint);
        }

        private void MissionComplete()
        {
            IsOnMission = false;
        }

        private IEnumerator WaitAnimationAndGoBase()
        {
            yield return _animator.SetGrabAnimation();
            _mover.SetPoint(_basePosition.position, OnArrivedAtBase);
        }
        
        private void OnArrivedAtPoint()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _grabber.Grab(_watermelonToGrab);
            _coroutine = StartCoroutine(WaitAnimationAndGoBase());
        }
        
        private void OnArrivedAtBase()
        {
            _grabber.Ungrab(_watermelonToGrab);
            _watermelonToGrab.Despawn();
            MissionComplete();
            MissionCompleted?.Invoke();
        }
    }
}
