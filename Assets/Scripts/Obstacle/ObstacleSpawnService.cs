using System.Collections;
using GameObjectPooling;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class ObstacleSpawnService : IObstacleSpawnService
    {
        private GameObject _prefab; // Префаб препятствия для спауна
        private Vector3 _spawnAreaMin; // Минимальные координаты области спауна
        private Vector3 _spawnAreaMax; // Максимальные координаты области спауна
        private Transform _parent; // Родительский объект для спауна препятствий
        private Coroutine _spawnCoroutine; // Короутина для спауна препятствий
        private MonoBehaviour _monoBehaviour; // MonoBehaviour для запуска короутины
        private float _spawnInterval; // Интервал между спаунами

        // Метод для инициализации спаунера
        public void InitializeSpawner(GameObject prefab, Vector3 spawnAreaMin, Vector3 spawnAreaMax, Transform parent,
            MonoBehaviour monoBehaviour, float spawnInterval)
        {
            _prefab = prefab;
            _spawnAreaMin = spawnAreaMin;
            _spawnAreaMax = spawnAreaMax;
            _parent = parent;
            _monoBehaviour = monoBehaviour;
            _spawnInterval = spawnInterval;
        }

        // Метод для начала спауна препятствий
        public void StartSpawning()
        {
            if (_spawnCoroutine == null)
            {
                _spawnCoroutine = _monoBehaviour.StartCoroutine(SpawnRoutine());
            }
        }

        // Метод для остановки спауна препятствий
        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                _monoBehaviour.StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        // Метод для перезапуска спауна препятствий
        public void RestartSpawning()
        {
            _spawnInterval = 1; // Сбрасываем интервал спауна
            StartSpawning();
        }

        // Короутина для спауна препятствий
        private IEnumerator SpawnRoutine()
        {
            while (Application.isPlaying)
            {
                Vector3 randomPosition = GetRandomPosition(); // Получаем случайную позицию для спауна
                SpawnObstacle(randomPosition); // Спауним препятствие

                yield return new WaitForSeconds(_spawnInterval); // Ждем заданный интервал

                if (_spawnInterval > 0.1f)
                    _spawnInterval = Mathf.Max(0.1f, _spawnInterval - 0.1f); // Уменьшаем интервал спауна
            }
        }

        // Метод для спауна препятствия в указанной позиции
        private void SpawnObstacle(Vector3 position)
        {
            ObstacleController obj = GameObjectPool
                .Instantiate(_prefab, position, Quaternion.identity, _parent)
                .GetComponent<ObstacleController>(); // Спауним объект из пула

            obj.ObstacleService.ActivateObstacle(); // Активируем препятствие
            obj.ObstacleService.MoveDown(); // Начинаем движение препятствия вниз
        }

        // Метод для получения случайной позиции в области спауна
        private Vector3 GetRandomPosition()
        {
            float x = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
            float y = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);
            float z = Random.Range(_spawnAreaMin.z, _spawnAreaMax.z);
            return _parent.position + new Vector3(x, y, z); // Возвращаем случайную позицию
        }
    }
}