using UnityEngine;

namespace StreetFight.Obstacle
{
    // Интерфейс для сервиса спауна препятствий
    public interface IObstacleSpawnService
    {
        // Метод для инициализации спаунера
        void InitializeSpawner(GameObject prefab, Vector3 spawnAreaMin, Vector3 spawnAreaMax, Transform parent,
            MonoBehaviour monoBehaviour, float spawnInterval);

        // Метод для начала спауна препятствий
        void StartSpawning();

        // Метод для остановки спауна препятствий
        void StopSpawning();

        // Метод для перезапуска спауна препятствий
        void RestartSpawning();
    }
}