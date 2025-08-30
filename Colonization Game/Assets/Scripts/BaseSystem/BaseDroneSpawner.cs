using Units;
using UnityEngine;

namespace BaseSystem
{
    public class BaseDroneSpawner : MonoBehaviour
    {
        [SerializeField] private Drone _dronePrefab;
        
        public int DroneCost { get; private set; } = 3;
        public int CountDrones { get; private set; }

        public Drone SpawnDrone()
        {
            CountDrones++;
            Drone drone = Instantiate(_dronePrefab, transform.position, Quaternion.identity);
            return drone;
        }
    }
}