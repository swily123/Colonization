using UnityEngine;

namespace SpawnerSystem
{
    public class Zoner : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _xDistance;
        [SerializeField, Min(0)] private float _zDistance;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_xDistance, 1, _zDistance));
        }

        public Vector3 GetRandomPoint()
        {
            float randomX = Random.Range(-_xDistance/2, _xDistance/2);
            float randomZ = Random.Range(-_zDistance/2, _zDistance/2);
            
            return new Vector3(randomX, 1, randomZ);
        }
    }
}