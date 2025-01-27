using System;
using System.Collections;
using System.Collections.Generic;
using GameObjectPooling;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StreetFight.Obstacle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        private GameObject obstaclePrefab; // Префаб препятствия
        
        [SerializeField] private Vector3 spawnAreaMin; // Нижний левый угол области спавна
        [SerializeField] private Vector3 spawnAreaMax; // Верхний правый угол области спавна
        [SerializeField] private float _spawnInterval = 1f; // Интервал между активацией объектов
        
        private IObstacleSpawnService _spawnService;
        private IPlayerService _playerService;

        [Inject]
        private void Construct(IObstacleSpawnService spawnService, IPlayerService playerService)
        {
            obstaclePrefab = Resources.Load<GameObject>("Prefabs/Obstacle/Obstacle");
            _spawnService = spawnService;
            _spawnService.InitializeSpawner(obstaclePrefab, spawnAreaMin, spawnAreaMax, transform, this, _spawnInterval);
            _playerService = playerService;
        }

        private void Start()
        {
            _spawnService.StartSpawning();
            if (_playerService != null)
            {
                _playerService.OnDeath += HandlePlayerDeath;
            }
        }

        private void OnDestroy()
        {
            if (_spawnService != null)
            {
                _spawnService.StopSpawning();
            }

            if (_playerService != null)
            {
                _playerService.OnDeath -= HandlePlayerDeath;
            }
        }
        
        private void HandlePlayerDeath()
        {
            Debug.Log("Player has died. Stopping obstacle spawner.");
            _spawnService.StopSpawning();
        }

        // Визуализация области спавна через Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f); // Полупрозрачный зелёный

            Vector3 center = transform.position + (spawnAreaMin + spawnAreaMax) / 2;
            Vector3 size = spawnAreaMax - spawnAreaMin;

            Gizmos.DrawCube(center, size);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, size);
        }
    }
}
