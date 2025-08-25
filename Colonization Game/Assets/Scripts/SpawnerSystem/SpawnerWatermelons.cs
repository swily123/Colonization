using System.Collections;
using Units;
using UnityEngine;
using UnityEngine.Pool;

namespace SpawnerSystem
{
    public class SpawnerWatermelons : MonoBehaviour
    {
        [SerializeField] private Zoner _zoner;
        [SerializeField] private Watermelon _watermelon;
        [SerializeField] private int _defaultCapacity;
        [SerializeField] private int _poolMaxSize;
        [SerializeField, Min(0)] private float _delay;
        
        private Coroutine _coroutine;
        private ObjectPool<Watermelon> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Watermelon>(
                createFunc: Spawn,
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _defaultCapacity,
                maxSize: _poolMaxSize);
        }

        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            StopSpawning();
            _coroutine = StartCoroutine(Spawning());
        }
        
        private void StopSpawning()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        
        private IEnumerator Spawning()
        {
            WaitForSeconds delay = new WaitForSeconds(_delay);
            
            while (enabled)
            {
                GetWatermelon();
                yield return delay;
            }
        }
        
        private Watermelon Spawn()
        {
            Watermelon watermelon = Instantiate(_watermelon, transform, false);

            return watermelon;
        }

        private void GetWatermelon()
        {
            if (_pool.CountActive < _poolMaxSize)
            {
                _pool.Get();
            }
        }
        
        private void ActionOnGet(Watermelon watermelon)
        {
            watermelon.gameObject.SetActive(true);
            watermelon.transform.SetParent(transform);
            watermelon.transform.localPosition = _zoner.GetRandomPoint();
            watermelon.ResetParameters();
            watermelon.DespawnRequested += ReleaseWatermelon;
        }

        private void ActionOnRelease(Watermelon watermelon)
        {
            watermelon.gameObject.SetActive(false);
            watermelon.DespawnRequested -= ReleaseWatermelon;
        }

        private void ReleaseWatermelon(Watermelon watermelon)
        {
            _pool.Release(watermelon);
        }
    }
}