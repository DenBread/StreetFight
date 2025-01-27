using UnityEngine;

namespace StreetFight.Obstacle
{
    public interface IObstacleSpawnService
    {
        void InitializeSpawner(GameObject prefab, Vector3 spawnAreaMin, Vector3 spawnAreaMax, Transform parent, MonoBehaviour monoBehaviour, float spawnInterval);
        void StartSpawning();
        void StopSpawning();
        void RestartSpawning();
    }
}