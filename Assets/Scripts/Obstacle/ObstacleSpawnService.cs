using System.Collections;
using GameObjectPooling;
using UnityEngine;

namespace StreetFight.Obstacle
{
    public class ObstacleSpawnService : IObstacleSpawnService
    {
        private GameObject _prefab;
        private Vector3 _spawnAreaMin;
        private Vector3 _spawnAreaMax;
        private Transform _parent;
        private Coroutine _spawnCoroutine;
        private MonoBehaviour _monoBehaviour;
        private float _spawnInterval;

        public void InitializeSpawner(GameObject prefab, Vector3 spawnAreaMin, Vector3 spawnAreaMax, Transform parent, MonoBehaviour monoBehaviour, float spawnInterval)
        {
            _prefab = prefab;
            _spawnAreaMin = spawnAreaMin;
            _spawnAreaMax = spawnAreaMax;
            _parent = parent;
            _monoBehaviour = monoBehaviour;
            _spawnInterval = spawnInterval;
        }

        public void StartSpawning()
        {
            if (_spawnCoroutine == null)
            {
                _spawnCoroutine = _monoBehaviour.StartCoroutine(SpawnRoutine());
            }
        }

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                _monoBehaviour.StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        public void RestartSpawning()
        {
            _spawnInterval = 1;
            StartSpawning();
        }

        private IEnumerator SpawnRoutine()
        {
            while (Application.isPlaying)
            {
                Vector3 randomPosition = GetRandomPosition();
                SpawnObstacle(randomPosition);

                yield return new WaitForSeconds(_spawnInterval);
                
                if (_spawnInterval > 0.1f)
                    _spawnInterval = Mathf.Max(0.1f, _spawnInterval - 0.1f);
            }
        }

        private void SpawnObstacle(Vector3 position)
        {
            ObstacleController obj = GameObjectPool
                .Instantiate(_prefab, position, Quaternion.identity, _parent)
                .GetComponent<ObstacleController>();
            
            obj.ObstacleService.ActivateObstacle();
            obj.ObstacleService.MoveDown();
        }

        private Vector3 GetRandomPosition()
        {
            float x = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
            float y = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);
            float z = Random.Range(_spawnAreaMin.z, _spawnAreaMax.z);
            return _parent.position + new Vector3(x, y, z);
        }
    }
}