using Reflex.Attributes;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        private GameObject obstaclePrefab; // Префаб препятствия

        [SerializeField] private Vector3 spawnAreaMin; // Нижний левый угол области спавна
        [SerializeField] private Vector3 spawnAreaMax; // Верхний правый угол области спавна
        [SerializeField] private float _spawnInterval = 1f; // Интервал между активацией объектов

        private IObstacleSpawnService _spawnService; // Сервис для спауна препятствий
        private IPlayerService _playerService; // Сервис для управления состоянием игрока

        [Inject]
        private void Construct(IObstacleSpawnService spawnService, IPlayerService playerService)
        {
            obstaclePrefab = Resources.Load<GameObject>("Prefabs/Obstacle/Obstacle"); // Загрузка префаба препятствия
            _spawnService = spawnService;
            _spawnService.InitializeSpawner(obstaclePrefab, spawnAreaMin, spawnAreaMax, transform, this,
                _spawnInterval); // Инициализация спаунера
            _playerService = playerService;
        }

        private void Start()
        {
            _spawnService.StartSpawning(); // Начало спауна препятствий
            if (_playerService != null)
            {
                _playerService.OnDeath += HandlePlayerDeath; // Подписка на событие смерти игрока
            }
        }

        private void OnDestroy()
        {
            if (_spawnService != null)
            {
                _spawnService.StopSpawning(); // Остановка спауна препятствий
            }

            if (_playerService != null)
            {
                _playerService.OnDeath -= HandlePlayerDeath; // Отписка от события смерти игрока
            }
        }

        private void HandlePlayerDeath()
        {
            Debug.Log("Player has died. Stopping obstacle spawner."); // Логирование смерти игрока
            _spawnService.StopSpawning(); // Остановка спауна препятствий при смерти игрока
        }

        // Визуализация области спавна через Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f); // Полупрозрачный зелёный

            Vector3 center = transform.position + (spawnAreaMin + spawnAreaMax) / 2; // Центр области спавна
            Vector3 size = spawnAreaMax - spawnAreaMin; // Размер области спавна

            Gizmos.DrawCube(center, size); // Рисование полупрозрачного куба
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, size); // Рисование контура куба
        }
    }
}